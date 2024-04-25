using Microsoft.EntityFrameworkCore;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Admin;
using Xp.FinancialPortfolioManager.Infrastructure.Common.Persistence;

namespace Xp.FinancialPortfolioManager.Infrastructure.Admins
{
    public class AdminsRepository(FinancialPortfolioDbContext _dbContext) : IAdminsRepository
    {        

        public async Task AddAdminAsync(Admin admin)
        {
            await _dbContext.Admins.AddAsync(admin);
        }

        public async Task<Admin?> GetByIdAsync(Guid adminId)
        {
            return await _dbContext.Admins
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == adminId);
        }

        public Task UpdateAsync(Admin admin)
        {
            _dbContext.Admins.Update(admin);

            return Task.CompletedTask;
        }
    }
}