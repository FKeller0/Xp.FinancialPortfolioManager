using Microsoft.EntityFrameworkCore;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Products;
using Xp.FinancialPortfolioManager.Infrastructure.Common.Persistence;

namespace Xp.FinancialPortfolioManager.Infrastructure.Products
{
    public class ProductsRepository(FinancialPortfolioDbContext _dbContext) : IProductsRepository
    {
        public async Task AddProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .AnyAsync(product => product.Id == id);
        }

        public async Task<Product?> GetByIdAsync(Guid ProductId)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(product => product.Id == ProductId);
        }

        public async Task<Product?> GetByNameAsync(string Name)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(product => product.Name == Name);
        }

        public async Task<List<Product>> ListProducts()
        {
            return await _dbContext.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public Task RemoveAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            return Task.CompletedTask;
        }        

        public Task UpdateAsync(Product product)
        {
            _dbContext.Update(product);
            return Task.CompletedTask;
        }
    }
}