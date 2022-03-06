using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public interface ISpendingRepository : IRepositoryBase<Spending>
    {

    }
    public class SpendingRepository : RepositoryBase<Spending>, ISpendingRepository
    {
        private readonly ApplicationDbContext _db;
        public SpendingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }
    }
}
