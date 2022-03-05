using DataLayer;
using DataLayer.Entities;
using DataLayer.Repositories;
using System;
using System.Threading.Tasks;

namespace Services
{
    public interface IMoneyUserService
    {
        Task<MoneyUser> GetAuthorByIdAsync(Guid id);
        MoneyUser GetByUserId(Guid userId);
        MoneyUser GetAuthorByAccessToken(string token);
    }

    public class MoneyUserService : IMoneyUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MoneyUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MoneyUser> GetAuthorByIdAsync(Guid id)
        {
            return await _unitOfWork.MoneyUsers.DbGetByIdAsync(id);
        }

        public MoneyUser GetByUserId(Guid userId)
        {
            return _unitOfWork.MoneyUsers.GetByUserId(userId);
        }

        public MoneyUser GetAuthorByAccessToken(string token)
        {
            return _unitOfWork.MoneyUsers.GetByToken(token);
        }

    }
}