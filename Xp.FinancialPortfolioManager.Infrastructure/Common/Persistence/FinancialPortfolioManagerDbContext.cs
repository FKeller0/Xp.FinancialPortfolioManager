using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Metadata;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Admin;
using Xp.FinancialPortfolioManager.Domain.Advisors;
using Xp.FinancialPortfolioManager.Domain.Clients;
using Xp.FinancialPortfolioManager.Domain.Products;
using Xp.FinancialPortfolioManager.Domain.Users;

namespace Xp.FinancialPortfolioManager.Infrastructure.Common.Persistence
{
    public class FinancialPortfolioDbContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Advisor> Advisors { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        public FinancialPortfolioDbContext(DbContextOptions options) : base(options) { }

        public async Task CommitChangesAsync()
        {
            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Advisor>()
                .HasMany(x => x.Clients)
                .WithOne(x => x.Advisor)
                .HasForeignKey(x => x.AdvisorId)
                .IsRequired();
        }
    }
}