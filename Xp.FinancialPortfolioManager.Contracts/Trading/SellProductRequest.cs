namespace Xp.FinancialPortfolioManager.Contracts.Trading
{
    public record SellProductRequest(Guid ClientPortfolioId, Guid ClientId, int Quantity, double Value);
}