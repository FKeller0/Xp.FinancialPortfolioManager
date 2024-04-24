using Microsoft.EntityFrameworkCore;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Users;
using Xp.FinancialPortfolioManager.Infrastructure.Common.Persistence;

namespace Xp.FinancialPortfolioManager.Infrastructure.Users
{
    public class UsersRepository(FinancialPortfolioDbContext _dbContext) : IUsersRepository
    {
        public async Task AddUserAsync(User user)
        {
            await _dbContext.AddAsync(user);
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _dbContext.Users.AnyAsync(user => user.Id == id);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _dbContext.Users.AnyAsync(user => user.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
        }

        public Task UpdateAsync(User user)
        {
            _dbContext.Update(user);

            return Task.CompletedTask;
        }
    }
}