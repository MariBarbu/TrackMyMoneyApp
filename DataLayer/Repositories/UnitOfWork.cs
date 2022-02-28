﻿using System;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IUnitOfWork
    {
        //IAppUserRepository Users { get; }
        //ITokenRepository Tokens { get; }
       


        Task<bool> SaveChangesAsync();
        public void LogDbTrack();

    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        //public IAppUserRepository Users { get; }
        //public ITokenRepository Tokens { get; }


        public UnitOfWork(ApplicationDbContext applicationDbContext
            /*IAppUserRepository userRepository, ITokenRepository tokenRepository*/)
        {
            _applicationDbContext = applicationDbContext;

            //Users = userRepository;
            //Tokens = tokenRepository;

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