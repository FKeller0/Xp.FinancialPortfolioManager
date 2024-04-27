using Xp.FinancialPortfolioManager.Domain.ClientPortfolios;

namespace Xp.FinancialPortfolioManager.Application.Common.Interfaces
{
    public interface IClientPorfolioRepository
    {
        Task AddProductToPortfolio(ClientPortfolio clientPortfolio);
        Task<ClientPortfolio?> GetByIdAsync(Guid clientPortfolioId);
        Task<List<ClientPortfolio>> ListClientPortfolio(Guid clientId);
        Task UpdateAsync(ClientPortfolio clientPortfolio);
        Task RemoveAsync(ClientPortfolio clientPortfolio);
    }
}