namespace Xp.FinancialPortfolioManager.Contracts.Trading
{
    public record BuyProductRequest(Guid ProductId, Guid ClientId, int Quantity, double Value);    
}