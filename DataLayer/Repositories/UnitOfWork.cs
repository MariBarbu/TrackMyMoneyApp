using System;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IUnitOfWork
    {
        IAppUserRepository Users { get; }
        ITokenRepository Tokens { get; }
        IMoneyUserRepository MoneyUsers { get;}
        IWishRepository Wishes { get; }
        IMonthRepository Months { get; }
        ISpendingRepository Spendings { get; }
        ICategoryRepository Categories { get;}



        Task<bool> SaveChangesAsync();
        public void LogDbTrack();

    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IAppUserRepository Users { get; }
        public ITokenRepository Tokens { get; }
        public IMoneyUserRepository MoneyUsers { get; set; }
        public IWishRepository Wishes { get; set; }
        public IMonthRepository Months { get; set; }
        public ISpendingRepository Spendings { get; set; }
        public ICategoryRepository Categories { get; set; }



        public UnitOfWork(ApplicationDbContext applicationDbContext,
            IAppUserRepository userRepository,
            ITokenRepository tokenRepository,
            IMoneyUserRepository moneyUserRepository,
            IWishRepository wishRepository,
            IMonthRepository monthRepository,
            ISpendingRepository spendingRepository,
            ICategoryRepository categoryRepository)
        {
            _applicationDbContext = applicationDbContext;

            Users = userRepository;
            Tokens = tokenRepository;
            MoneyUsers = moneyUserRepository;
            Wishes = wishRepository;
            Months = monthRepository;
            Spendings = spendingRepository;
            Categories = categoryRepository;

        }

        public void LogDbTrack()
        {
            foreach (var entry in _applicationDbContext.ChangeTracker.Entries()/*.Where(e => e.State == EntityState.Modified)*/)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: { entry.State}");
            }
        }
        public async Task<bool> SaveChangesAsync()
        {
            LogDbTrack();
            try
            {
                var save = await _applicationDbContext.SaveChangesAsync();
                if (save <= 0) return false;
                return (save >= 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new BadRequestException("CANNOT_UPDATE_DATABASE");
            }

        }
    }
}