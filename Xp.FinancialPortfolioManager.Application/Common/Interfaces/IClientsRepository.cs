using Xp.FinancialPortfolioManager.Domain.Clients;

namespace Xp.FinancialPortfolioManager.Application.Common.Interfaces
{
    public interface IClientsRepository
    {
        Task AddClientAsync(Client client);
        Task<bool> ExistsAsync(Guid id);
        Task<Client?> GetByIdAsync(Guid clientId);
        Task<List<Client>> ListByAdvisorIdAsync(Guid advisorId);
        Task UpdateAsync(Client client);
    }
}