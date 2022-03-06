using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IWishService
    {

    }
    public class WishService : IWishService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WishService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
