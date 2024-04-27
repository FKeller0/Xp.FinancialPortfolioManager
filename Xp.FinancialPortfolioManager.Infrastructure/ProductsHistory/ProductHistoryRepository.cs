using Microsoft.EntityFrameworkCore;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.ProductsHistory;
using Xp.FinancialPortfolioManager.Infrastructure.Common.Persistence;

namespace Xp.FinancialPortfolioManager.Infrastructure.ProductsHistory
{
    public class ProductHistoryRepository(FinancialPortfolioDbContext dbContext) : IProductHistoryRepository
    {
        private readonly FinancialPortfolioDbContext _dbContext = dbContext;

        public async Task AddProductTradeHistoryAsync(ProductHistory history)
        {
            await _dbContext.ProductsHistory.AddAsync(history);
        }

        public async Task<List<ProductHistory>> ListProductHistory(Guid productId)
        {
            return await _dbContext.ProductsHistory
                .AsNoTracking()
                .Where(history => history.ProductId == productId)
                .ToListAsync();
        }
    }
}