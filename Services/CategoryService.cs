using AutoMapper;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Repositories;
using Services.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICategoryService
    {
        Task<bool> AddCategoryAsync(AddCategoryDto category, MoneyUser moneyUser);
        Task<GetCategoryDto> GetCategoryByIdAsync(Guid id);
        List<GetCategoryDto> GetAllCategories(MoneyUser moneyUser);
        Task<bool> DeleteCategoryAsync(Guid categoryId);
    }
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddCategoryAsync(AddCategoryDto category, MoneyUser moneyUser)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var oldCategory = _unitOfWork.Categories.GetByName(category.Name);
            if (oldCategory!= null)
                throw new BadRequestException(ErrorService.CategoryAlreadyExist);
            var newCategory = _mapper.Map<Category>(category);
            newCategory.MoneyUserId = moneyUser.Id;
            _unitOfWork.Categories.Insert(newCategory);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<GetCategoryDto> GetCategoryByIdAsync(Guid id)
        {
            var category = await _unitOfWork.Categories.DbGetByIdAsync(id);
            if (category== null)
                throw new BadRequestException(ErrorService.CategoryNotFound);
            var result = _mapper.Map<GetCategoryDto>(category);
            return result;
        }

        public List<GetCategoryDto> GetAllCategories(MoneyUser moneyUser)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var categories = _unitOfWork.Categories.GetAllByMoneyUser(moneyUser.Id);
            var categoriesDto = _mapper.Map<List<GetCategoryDto>>(categories);
            
            return categoriesDto;
        }

        public async Task<bool> DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _unitOfWork.Categories.DbGetByIdAsync(categoryId);
            if (category == null)
                throw new BadRequestException(ErrorService.CategoryNotFound);
            _unitOfWork.Categories.Delete(category);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
