namespace Xp.FinancialPortfolioManager.Contracts.Products
{
    public record ProductResponse(
        Guid Id,
        string Name,
        string Description,
        double Value,
        DateTime ExpiresAt,
        DateTime CreatedAt);
}