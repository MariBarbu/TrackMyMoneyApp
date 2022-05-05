using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Category GetByName(string name);
        List<Category> GetAllByMoneyUser(Guid moneyUserId);
        Category GetWithSpendings(Guid categoryId);
        Category GetByNameAndMoneyUser(string name, Guid moneyUserId);
    }
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public Category GetByName(string name)
        {
            return DbGetRecords()
                .FirstOrDefault(c => c.Name == name);
        }
        public Category GetByNameAndMoneyUser(string name, Guid moneyUserId)
        {
            return DbGetRecords()
                .FirstOrDefault(c => c.Name == name && c.MoneyUserId == moneyUserId);
        }

        public List<Category> GetAllByMoneyUser(Guid moneyUserId)
        {
            return DbGetRecords().Where(c => c.MoneyUserId == moneyUserId).ToList();
        }

        public Category GetWithSpendings(Guid categoryId)
        {
            return DbGetRecords().Include(c => c.Spendings).FirstOrDefault(c => c.Id == categoryId);
        }
    }
}
