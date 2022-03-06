using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public interface IMonthRepository : IRepositoryBase<Month>
    {

    }
    public class MonthRepository : RepositoryBase<Month>, IMonthRepository
    {
        private readonly ApplicationDbContext _db;
        public MonthRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }
    }
}
