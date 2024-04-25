namespace Xp.FinancialPortfolioManager.Contracts.Products
{
    public record CreateProductRequest(
        string Name,
        string Description,
        double Value,
        DateTime ExpiresAt);    
}