namespace Xp.FinancialPortfolioManager.Contracts.Products
{
    public record ProductResponse(
        Guid Id,
        string Name,
        string Description,        
        DateTime ExpiresAt,
        DateTime CreatedAt);
}