namespace Xp.FinancialPortfolioManager.Contracts.Products
{
    public record CreateProductRequest(
        string Name,
        string Description,
        DateTime ExpiresAt);    
}