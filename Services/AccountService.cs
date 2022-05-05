using AutoMapper;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Entities.Enums;
using DataLayer.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Dtos.Profile;
using Services.Dtos.User;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAccountService
    {
        Task<Token> GenerateJwtToken(AppUser user);
        Task<UserInformationDto> LoginAsync(LoginRequestDto loginRequest);
        Task<MoneyUser> RegisterUserMoneyAsync(RegisterRequestDto registerRequest);
        UserInformationDto GetUserInformation(Guid id);
        Task<AppUser> RegisterAdmin(RegisterRequestDto registerRequest);
        GetProfileDto GetProfile(MoneyUser moneyUser);
        Task<bool> EditProfile(MoneyUser moneyUser, EditProfileDto profile);
    }
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserAuthenticationHelper _userAuthentificationHelper;
        private readonly IMapper _mapper;
        public AccountService(IUnitOfWork unitOfWork, IConfiguration configuration,
            IAppUserRepository appUserRepository, IUserAuthenticationHelper userAuthentificationHelper,
            IRoleRepository roleRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _appUserRepository = appUserRepository;
            _userAuthentificationHelper = userAuthentificationHelper;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<MoneyUser> RegisterUserMoneyAsync(RegisterRequestDto registerRequest)
        {
            var moneyUser = await _userAuthentificationHelper.CreateUserAndMoneyUserAsync(registerRequest);
            return moneyUser;
        }
        public async Task<AppUser> RegisterAdmin(RegisterRequestDto registerRequest)
        {
            var user = await _userAuthentificationHelper.CreateUserAsync(registerRequest, AppUserTypes.Admin);
            return user;
        }
        public async Task<Token> GenerateJwtToken(AppUser user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = await _appUserRepository.GetClaimsAsync(user);
            var roles = await _appUserRepository.GetUserRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));

                var r = await _roleRepository.GetRoleByNameAsync(role);
                var roleClaims = await _roleRepository.GetClaimsByRoleAsync(r);

                foreach (var roleClaim in roleClaims)
                {
                    claims.Add(roleClaim);
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddYears(1),
                SigningCredentials = credentials
            };

            var token = handler.CreateToken(tokenDescriptor);
            var tokenString = handler.WriteToken(token);
            var accessToken = new Token
            {
                TokenString = tokenString,
                ExpireDate = tokenDescriptor.Expires.Value,
                IsRevoked = false,
                Type = TokenTypes.AccessToken,
                UserId = user.Id
            };
            return accessToken;
        }

        public async Task<UserInformationDto> LoginAsync(LoginRequestDto loginRequest)
        {
            var user = await _appUserRepository.FindUserByEmailAsync(loginRequest.Email, asNoTracking: false);
            _userAuthentificationHelper.VerifyUser(user);

            var result = await _appUserRepository.SignInAsync(loginRequest.Email, loginRequest.Password);
            if (!result.Succeeded) throw new BadRequestException(ErrorService.InvalidLogin);

            var accessToken = await GenerateJwtToken(user);
            _unitOfWork.Tokens.Insert(accessToken);
            await _unitOfWork.SaveChangesAsync();
            return _userAuthentificationHelper.UserSignedInAsync(user, loginRequest, accessToken);
        }
        public UserInformationDto GetUserInformation(Guid id)
        {
            var user = _appUserRepository.GetById(id, asNoTracking: true);
            if (user == null)
            {
                throw new BadRequestException(ErrorService.NoUserFound);
            }
            return _userAuthentificationHelper.GetUserInformationByTypeAsync(user);
        }

        public GetProfileDto GetProfile(MoneyUser moneyUser)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var profile = _mapper.Map<GetProfileDto>(moneyUser);
            return profile;
        }
        public async Task<bool> EditProfile(MoneyUser moneyUser, EditProfileDto profile)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);

            var dbMoneyUser = _unitOfWork.MoneyUsers.GetByUserId(moneyUser.UserId);
            dbMoneyUser.User.Email = profile.Email;
            dbMoneyUser.User.FirstName = profile.FirstName;
            dbMoneyUser.User.LastName = profile.LastName;
            dbMoneyUser.BirthDate = profile.BirthDate;
            _unitOfWork.MoneyUsers.Update(dbMoneyUser);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}