using Microsoft.EntityFrameworkCore;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.ClientsHistory;
using Xp.FinancialPortfolioManager.Infrastructure.Common.Persistence;

namespace Xp.FinancialPortfolioManager.Infrastructure.ClientsHistory
{
    public class ClientHistoryRepository(FinancialPortfolioDbContext dbContext) : IClientsHistoryRepository
    {
        private readonly FinancialPortfolioDbContext _dbContext = dbContext;

        public async Task AddClientTradeHistoryAsync(ClientHistory history)
        {
            await _dbContext.ClientsHistory.AddAsync(history);
        }

        public async Task<List<ClientHistory>> ListClientHistory(Guid clientId)
        {
            return await _dbContext.ClientsHistory
                .AsNoTracking()
                .Where(history => history.ClientId == clientId)
                .ToListAsync();
        }
    }
}