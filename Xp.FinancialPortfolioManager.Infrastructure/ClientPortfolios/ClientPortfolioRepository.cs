using Microsoft.EntityFrameworkCore;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.ClientPortfolios;
using Xp.FinancialPortfolioManager.Infrastructure.Common.Persistence;

namespace Xp.FinancialPortfolioManager.Infrastructure.ClientPortfolios
{
    public class ClientPortfolioRepository(FinancialPortfolioDbContext _dbContext) : IClientPorfolioRepository
    {
        public async Task AddProductToPortfolio(ClientPortfolio product)
        {
            await _dbContext.ClientPortfolio.AddAsync(product);
        }

        public async Task<ClientPortfolio?> GetByIdAsync(Guid ClientPortfolioId)
        {
            return await _dbContext.ClientPortfolio.FirstOrDefaultAsync(clientPortfolio => clientPortfolio.Id == ClientPortfolioId);
        }

        public async Task<List<ClientPortfolio>> ListClientPortfolio(Guid clientId)
        {
            return await _dbContext.ClientPortfolio
                .AsNoTracking()
                .Where(portfolio => portfolio.ClientId == clientId)
                .ToListAsync();
        }

        public Task UpdateAsync(ClientPortfolio clientPortfolio)
        {
            _dbContext.Update(clientPortfolio);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(ClientPortfolio clientPortfolio)
        {
            _dbContext.ClientPortfolio.Remove(clientPortfolio);
            return Task.CompletedTask;
        }
    }
}