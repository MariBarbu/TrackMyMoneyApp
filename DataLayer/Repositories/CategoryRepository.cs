using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {

    }
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }
    }
}
