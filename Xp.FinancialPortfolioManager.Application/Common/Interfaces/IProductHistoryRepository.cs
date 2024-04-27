using Xp.FinancialPortfolioManager.Domain.ProductsHistory;

namespace Xp.FinancialPortfolioManager.Application.Common.Interfaces
{
    public interface IProductHistoryRepository
    {
        Task AddProductTradeHistoryAsync(ProductHistory history);
        Task<List<ProductHistory>> ListProductHistory(Guid clientId);
    }
}