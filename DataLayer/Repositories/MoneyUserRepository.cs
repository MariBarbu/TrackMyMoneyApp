using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IMoneyUserRepository : IRepositoryBase<MoneyUser>
    {
        MoneyUser GetByUserId(Guid userId);
        MoneyUser GetByToken(string token);
    }
    public class MoneyUserRepository : RepositoryBase<MoneyUser>, IMoneyUserRepository
    {
        private readonly ApplicationDbContext _db;
        public MoneyUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public MoneyUser GetByUserId(Guid userId)
        {
            return DbGetRecords().Include(a => a.User).FirstOrDefault(a => a.UserId == userId);
        }

        public MoneyUser GetByToken(string token)
        {
            return DbGetRecords()
                .Include(a => a.User)
                    .ThenInclude(u => u.Tokens)
                .FirstOrDefault(a => a.User.Tokens.Any(t => t.Type == TokenTypes.AccessToken && t.TokenString == token));
        }
    }
}