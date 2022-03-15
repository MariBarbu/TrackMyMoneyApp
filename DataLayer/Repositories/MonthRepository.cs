using DataLayer.Entities;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public interface IMonthRepository : IRepositoryBase<Month>
    {
        Month GetCurrentMonth(Guid moneyUserId);
    }
    public class MonthRepository : RepositoryBase<Month>, IMonthRepository
    {
        private readonly ApplicationDbContext _db;
        public MonthRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public Month GetCurrentMonth(Guid moneyUserId)
        {
            return DbGetRecords()
                .Include(m => m.Spendings)
                .FirstOrDefault(m => m.MonthOfYear == DateTime.UtcNow.Month && m.Year == DateTime.UtcNow.Year && m.MoneyUserId == moneyUserId);
        }


    }
}
