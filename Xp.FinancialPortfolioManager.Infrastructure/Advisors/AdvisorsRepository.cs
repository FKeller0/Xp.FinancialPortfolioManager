using Microsoft.EntityFrameworkCore;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Advisors;
using Xp.FinancialPortfolioManager.Infrastructure.Common.Persistence;

namespace Xp.FinancialPortfolioManager.Infrastructure.Advisors
{
    public class AdvisorsRepository(FinancialPortfolioDbContext _dbContext) : IAdvisorsRepository
    {
        public async Task AddAdvisorAsync(Advisor advisor)
        {
            await _dbContext.Advisors.AddAsync(advisor);
        }

        public async Task<Advisor?> GetByIdAsync(Guid adminId)
        {
            return await _dbContext.Advisors
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == adminId);
        }

        public async Task<Advisor?> GetByUserIdAsync(Guid userId)
        {
            return await _dbContext.Advisors
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<List<Advisor>> ListAdvisors() 
        {
            return await _dbContext.Advisors
                .AsNoTracking()
                .ToListAsync();
        }

        public Task UpdateAsync(Advisor admin)
        {
            _dbContext.Advisors.Update(admin);

            return Task.CompletedTask;
        }
    }
}