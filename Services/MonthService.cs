using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IMonthService
    {

    }
    public class MonthService : IMonthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MonthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
