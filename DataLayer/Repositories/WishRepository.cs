using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public interface IWishRepository : IRepositoryBase<Wish>
    {

    }
    public class WishRepository : RepositoryBase<Wish>, IWishRepository
    {
        private readonly ApplicationDbContext _db;
        public WishRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }
    }
}
