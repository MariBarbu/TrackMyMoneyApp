using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    public interface IMonthRepository : IRepositoryBase<Month>
    {
        Month GetCurrentMonth();
    }
    public class MonthRepository : RepositoryBase<Month>, IMonthRepository
    {
        private readonly ApplicationDbContext _db;
        public MonthRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public Month GetCurrentMonth()
        {
            return DbGetRecords()
                .FirstOrDefault(m => m.MonthOfYear == DateTime.UtcNow.Month && m.Year == DateTime.UtcNow.Year);
        }
    }
}
