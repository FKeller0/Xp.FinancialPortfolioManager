using Xp.FinancialPortfolioManager.Domain.Products;

namespace Xp.FinancialPortfolioManager.Application.Common.Interfaces
{
    public interface IProductsRepository
    {
        Task AddProductAsync(Product product);
        Task<bool> ExistsAsync(Guid id);
        Task<Product?> GetByIdAsync(Guid ProductId);
        Task<Product?> GetByNameAsync(string Name);
        Task<List<Product>> ListProducts();
        Task UpdateAsync(Product product);
        Task RemoveAsync(Product product);
    }
}