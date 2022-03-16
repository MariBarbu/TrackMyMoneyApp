using MoneyXamarin.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyXamarin.Services
{
    public interface IWishService
    {
        Task<IEnumerable<Wish>> GetWishes();
        Task<Wish> GetWish(Guid id);
        Task AddWish(Wish wish);
        Task DeleteWish(Wish wish);
    }
}
