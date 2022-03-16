using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    public interface IWishRepository : IRepositoryBase<Wish>
    {
        List<Wish> GetAllByMoneyUser(Guid moneyUserId);
        Wish GetWishByName(string name, Guid moneyUserId);
    }
    public class WishRepository : RepositoryBase<Wish>, IWishRepository
    {
        private readonly ApplicationDbContext _db;
        public WishRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public List<Wish> GetAllByMoneyUser(Guid moneyUserId)
        {
            return DbGetRecords().Where(w => w.MoneyUserId == moneyUserId).ToList();
        }

        public Wish GetWishByName(string name, Guid moneyUserId)
        {
            return DbGetRecords().FirstOrDefault(w => w.Name == name && w.MoneyUserId == moneyUserId);
        }
    }
}
