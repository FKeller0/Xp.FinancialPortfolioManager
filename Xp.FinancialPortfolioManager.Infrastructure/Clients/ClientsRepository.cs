using Microsoft.EntityFrameworkCore;
using OneOf.Types;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Clients;
using Xp.FinancialPortfolioManager.Infrastructure.Common.Persistence;

namespace Xp.FinancialPortfolioManager.Infrastructure.Clients
{
    public class ClientsRepository(FinancialPortfolioDbContext dbContext) : IClientsRepository
    {
        private readonly FinancialPortfolioDbContext _dbContext = dbContext;

        public async Task AddClientAsync(Client client)
        {
            await _dbContext.Clients.AddAsync(client);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbContext.Clients
                .AsNoTracking()
                .AnyAsync(client => client.Id == id);
        }

        public async Task<Client?> GetByIdAsync(Guid clientId)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(client => client.Id == clientId);
        }

        public async Task<List<Client>> ListByAdvisorIdAsync(Guid advisorId)
        {
            return await _dbContext.Clients
                .AsNoTracking()
                .Where(client => client.AdvisorId == advisorId)
                .ToListAsync();
        }

        public Task UpdateAsync(Client client)
        {
            _dbContext.Update(client);
            return Task.CompletedTask;
        }
    }
}