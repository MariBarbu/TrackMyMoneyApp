using DataLayer;
using DataLayer.Entities;
using DataLayer.Entities.Enums;
using DataLayer.Repositories;
using Services.Dtos;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserAuthenticationHelper
    {
        UserInformationDto GetUserInformationByTypeAsync(AppUser user);
        void VerifyUser(AppUser user);
        UserInformationDto UserSignedInAsync(AppUser user, LoginRequestDto loginRequest, Token accessToken);
        Task<MoneyUser> CreateUserAndMoneyUserAsync(RegisterRequestDto registerRequest);
        Task<AppUser> CreateUserAsync(RegisterRequestDto request, AppUserTypes type);
    }
    public class UserAuthenticationHelper : IUserAuthenticationHelper
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserRepository _appUserRepository;

        public UserAuthenticationHelper(IUnitOfWork unitOfWork, IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
            _unitOfWork = unitOfWork;

        }
        private async Task AddClaimsToUserAsync(AppUser user, string customName, Guid id)
        {
            await _appUserRepository.AddClaimToUserAsync(user, new Claim(ClaimTypes.Email, user.Email));
            await _appUserRepository.AddClaimToUserAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            await _appUserRepository.AddClaimToUserAsync(user, new Claim(ClaimTypes.Name, user.FirstName));
            await _appUserRepository.AddClaimToUserAsync(user, new Claim(ClaimTypes.Surname, user.LastName));
            await _appUserRepository.AddClaimToUserAsync(user, new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
            await _appUserRepository.AddClaimToUserAsync(user, new Claim(customName, id.ToString()));
        }
        public UserInformationDto GetUserInformationByTypeAsync(AppUser user)
        {
            var userInfo = _appUserRepository.GetById(user.Id);
            if (userInfo != null)
            {
                var result = new UserInformationDto
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    Email = userInfo.Email
                };

                return result;

            }


            throw new BadRequestException(ErrorService.NoUserFound);
        }

        public void VerifyUser(AppUser user)
        {
            if (user == null)
            {
                throw new BadRequestException(ErrorService.InvalidLogin);
            }

        }

        public UserInformationDto UserSignedInAsync(AppUser user, LoginRequestDto loginRequest, Token accessToken)
        {
            var moneyUser = _unitOfWork.MoneyUsers.GetByUserId(user.Id);
            if (moneyUser == null && user.Type != AppUserTypes.Admin)
                throw new BadRequestException(ErrorService.NoUserFound);

            UserInformationDto login = new UserInformationDto
            {
                Email = loginRequest.Email,
                AccessToken = accessToken.TokenString,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return login;
        }

        public async Task<MoneyUser> CreateUserAndMoneyUserAsync(RegisterRequestDto registerRequest)
        {
            var user = await CreateUserAsync(registerRequest, AppUserTypes.MoneyUser);
            var newMoneyUser = new MoneyUser
            {
                User = user,
                UserId = user.Id,
                BirthDate = registerRequest.BirthDate,
                Address = registerRequest.Address
            };
            _unitOfWork.MoneyUsers.Insert(newMoneyUser);
            await AddClaimsToUserAsync(user, "moneyUserId", newMoneyUser.Id);
            return newMoneyUser;
        }

        public async Task<AppUser> CreateUserAsync(RegisterRequestDto request, AppUserTypes type)
        {

            var oldUser = await _appUserRepository.FindUserByEmailAsync(request.Email, asNoTracking: true);
            if (oldUser != null) throw new BadRequestException(ErrorService.UserAlreadyExists);

            var user = new AppUser
            {
                Type = type,
                Email = request.Email,
                UserName = request.Email,
                LastName = request.LastName,
                FirstName = request.FirstName,
                PhoneNumber = request.PhoneNumber,
                ValidationToken = Guid.NewGuid().ToString()
            };

            var result = await _appUserRepository.CreateUserAsync(user, request.Password);
            if (!result.Succeeded) throw new Exception(ErrorService.UserNotCreated);

            var roleResult = await _appUserRepository.AddUserToRoleAsync(user, type.ToString());
            if (roleResult == null) throw new Exception(ErrorService.RoleNotAdded);

            return user;
        }
    }
}