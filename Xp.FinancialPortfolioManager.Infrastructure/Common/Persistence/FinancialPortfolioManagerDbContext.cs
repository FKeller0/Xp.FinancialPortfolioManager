using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Users;

namespace Xp.FinancialPortfolioManager.Infrastructure.Common.Persistence
{
    public class FinancialPortfolioDbContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; } = null!;

        public FinancialPortfolioDbContext(DbContextOptions options) : base(options) { }

        public async Task CommitChangesAsync()
        {
            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

        }
    }
}