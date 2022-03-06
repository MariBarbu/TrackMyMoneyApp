using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface ISpendingService
    {

    }
    public class SpendingService : ISpendingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpendingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
