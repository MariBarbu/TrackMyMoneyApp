using AutoMapper;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Entities.Eums;
using DataLayer.Repositories;
using Services.Dtos.Wish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IWishService
    {
        List<GetWishDto> GetUserWishes(MoneyUser moneyUser);
        Task<GetWishDto> GetWishAsync(Guid wishId);
        Task<bool> AddWishAsync(AddWishDto wish, MoneyUser moneyUser);
        Task<bool> CheckWishAsync(Guid wishId);
        Task<bool> UncheckWishAsync(Guid wishId);
        Task<bool> DeleteWishAsync(Guid wishId);
        Task<List<GetWishDto>> GetAllWishes();
    }
    public class WishService : IWishService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WishService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<GetWishDto> GetUserWishes(MoneyUser moneyUser)
        {
            var result = new GetWishesDto();
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var wishes = _unitOfWork.Wishes.GetAllByMoneyUser(moneyUser.Id);
            var wishesDto = _mapper.Map<List<GetWishDto>>(wishes);
           return wishesDto;
        }

        public async Task<List<GetWishDto>> GetAllWishes()
        {
            
           
            var wishes = await _unitOfWork.Wishes.DbGetAllAsync();
            var wishesDto = _mapper.Map<List<GetWishDto>>(wishes);
            
            return wishesDto;
        }

        public async Task<GetWishDto> GetWishAsync(Guid wishId)
        {

            var wish = await _unitOfWork.Wishes.DbGetByIdAsync(wishId);
            if (wish == null)
                throw new BadRequestException(ErrorService.WishNotFound);

            var wishDto = _mapper.Map<GetWishDto>(wish);
            return wishDto;
        }

        public async Task<bool> AddWishAsync(AddWishDto wish, MoneyUser moneyUser)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var oldWish = _unitOfWork.Wishes.GetWishByName(wish.Name, moneyUser.Id);
            if(oldWish == null)
            {
                var newWish = _mapper.Map<Wish>(wish);
                newWish.MoneyUserId = moneyUser.Id;
                _unitOfWork.Wishes.Insert(newWish);
            }
            else
            {
                oldWish.Name = wish.Name;
                oldWish.Description = wish.Description;
                oldWish.Price = wish.Price;
                _unitOfWork.Wishes.Update(oldWish);
            }

            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> CheckWishAsync(Guid wishId)
        {
            var wish = await _unitOfWork.Wishes.DbGetByIdAsync(wishId);
            if (wish == null)
                throw new BadRequestException(ErrorService.WishNotFound);
            
            var moneyUser = await _unitOfWork.MoneyUsers.DbGetByIdAsync(wish.MoneyUserId);
            var currentMonth = _unitOfWork.Months.GetCurrentMonth(moneyUser.Id);
            if (wish.Price > moneyUser.Economies)
                throw new BadRequestException(ErrorService.NotEnoughMoney);
            wish.Status = WishStatus.Checked;
            moneyUser.Economies -= wish.Price;
            _unitOfWork.Wishes.Update(wish);
            _unitOfWork.MoneyUsers.Update(moneyUser);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UncheckWishAsync(Guid wishId)
        {
            var wish = await _unitOfWork.Wishes.DbGetByIdAsync(wishId);
            if (wish == null)
                throw new BadRequestException(ErrorService.WishNotFound);
            wish.Status = WishStatus.Active;
            var moneyUser = await _unitOfWork.MoneyUsers.DbGetByIdAsync(wish.MoneyUserId);
            
            moneyUser.Economies += wish.Price;
            _unitOfWork.Wishes.Update(wish);
            _unitOfWork.MoneyUsers.Update(moneyUser);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteWishAsync(Guid wishId) 
        {
            var wish = await _unitOfWork.Wishes.DbGetByIdAsync(wishId);
            if (wish == null)
                throw new BadRequestException(ErrorService.WishNotFound);
            _unitOfWork.Wishes.Delete(wish);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
