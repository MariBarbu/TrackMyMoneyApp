using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataLayer
{
    public class ApplicationDbContext : IdentityDbContext <AppUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Token> Tokens { get; set; }  
        public DbSet<MoneyUser> MoneyUsers { get; set; } 
        public DbSet<Wish> Wishes { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Spending> Spendings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            AddBaseEntityQueryFilter(builder, "DeletedAt", null);
        }
        private void AddBaseEntityQueryFilter(ModelBuilder builder, string property, object value)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var type = entityType.ClrType;
                if (!type.IsSubclassOf(typeof(BaseEntity))) continue;

                var param = Expression.Parameter(type);
                var filter = Expression.Lambda(
                    Expression.Equal(Expression.Property(param, property), Expression.Constant(value)),
                        param);
                builder.Entity(type).HasQueryFilter(filter);
            }
        }
    }
}