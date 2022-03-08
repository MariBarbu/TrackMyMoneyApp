using AutoMapper;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Repositories;
using Services.Dtos.Wish;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IWishService
    {
        GetWishesDto GetUserWishes(MoneyUser moneyUser);
        Task<bool> AddWish(AddWishDto wish, MoneyUser moneyUser);
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

        public GetWishesDto GetUserWishes(MoneyUser moneyUser)
        {
            var result = new GetWishesDto();
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var wishes = _unitOfWork.Wishes.GetAllByMoneyUser(moneyUser.Id);
            var wishesDto = _mapper.Map<List<GetWishDto>>(wishes);
            result.Wishes = wishesDto;
            return result;
        }

        public Task<bool> AddWish(AddWishDto wish, MoneyUser moneyUser)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);

            var oldWish = _unitOfWork.Wishes.GetWishByName(wish.Name, moneyUser.Id);
            if(oldWish == null)
            {
                var newWish = _mapper.Map<Wish>(wish);
                _unitOfWork.Wishes.Insert(newWish);
            }
            else
            {
                oldWish.Name = wish.Name;
                oldWish.Description = wish.Description;
                oldWish.Price = wish.Price;
                _unitOfWork.Wishes.Update(oldWish);
            }

            return _unitOfWork.SaveChangesAsync();
        }

        public Task<bool> CheckWish()
        {

        }

        public Task<bool> UncheckWish()
        {

        }

        public Task<bool> DeleteWish()
        {

        }
    }
}
