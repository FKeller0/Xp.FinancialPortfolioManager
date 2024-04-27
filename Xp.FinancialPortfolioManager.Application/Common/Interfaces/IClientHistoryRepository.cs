using Xp.FinancialPortfolioManager.Domain.ClientsHistory;

namespace Xp.FinancialPortfolioManager.Application.Common.Interfaces
{
    public interface IClientsHistoryRepository
    {
        Task AddClientTradeHistoryAsync(ClientHistory history);
        Task<List<ClientHistory>> ListClientHistory(Guid clientId);
    }
}