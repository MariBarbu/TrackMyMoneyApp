using DataLayer.Entities;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataLayer.Repositories
{
    public interface IMonthRepository : IRepositoryBase<Month>
    {
        Month GetCurrentMonth(Guid moneyUserId);
        List<Month> GetMonthByYear(int year, Guid moneyUserId);
        Month GetMonthByYearAndMonth(int year, int month, Guid moneyUserId);
        List<Month> GetAllByUser(Guid moneyUserId);
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

        public Month GetMonthByYearAndMonth(int year, int month, Guid moneyUserId)
        {
            return DbGetRecords()
                .Include(m => m.Spendings)
                .FirstOrDefault(m => m.Year == year && m.MonthOfYear == month && m.MoneyUserId == moneyUserId);
        }

        public List<Month> GetMonthByYear(int year, Guid moneyUserId)
        {
            return DbGetRecords()
                .Include(m => m.Spendings)
                .Where(m => m.Year == year && m.MoneyUserId == moneyUserId).ToList();
        }

        public List<Month> GetAllByUser(Guid moneyUserId)
        {
            return DbGetRecords()
                .Where(m => m.MoneyUserId == moneyUserId).ToList();
        }


    }
}
