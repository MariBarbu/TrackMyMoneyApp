using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface ICategoryService
    {

    }
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
