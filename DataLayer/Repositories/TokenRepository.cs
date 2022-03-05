using System;
using System.Linq;
using DataLayer.Entities;

namespace DataLayer.Repositories
{
    public interface ITokenRepository : IRepositoryBase<Token>
    {
        Token GetAccessTokenByTokenString(string tokenString);
        Token GetAccessTokenByUserId(Guid id);
    }

    public class TokenRepository : RepositoryBase<Token>, ITokenRepository
    {
        public TokenRepository(ApplicationDbContext db) : base(db)
        {
        }

        public Token GetAccessTokenByTokenString(string tokenString)
        {
            return DbGetRecords()
                .Where(t => t.Type == TokenTypes.AccessToken)
                .FirstOrDefault(t => t.TokenString == tokenString);
        }

        public Token GetAccessTokenByUserId(Guid id)
        {
            return DbGetRecords()
                .Where(t => t.UserId == id)
                .FirstOrDefault(t => t.Type == TokenTypes.AccessToken);
        }
    }
}