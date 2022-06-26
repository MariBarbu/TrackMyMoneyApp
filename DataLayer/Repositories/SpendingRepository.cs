using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    public interface ISpendingRepository : IRepositoryBase<Spending>
    {
        Spending GetById(Guid id);
    }
    public class SpendingRepository : RepositoryBase<Spending>, ISpendingRepository
    {
        private readonly ApplicationDbContext _db;
        public SpendingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public Spending GetById(Guid id)
        {
            return DbGetRecords()
                .Include(s => s.Month)
                .FirstOrDefault(s => s.Id == id);
        }
    }
}
