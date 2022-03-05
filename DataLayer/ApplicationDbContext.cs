using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataLayer
{
    public class ApplicationDbContext : IdentityDbContext <AppUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Token> Tokens { get; set; }
        public DbSet<MoneyUser> MoneyUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}