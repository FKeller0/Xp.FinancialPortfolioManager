namespace Xp.FinancialPortfolioManager.Contracts.Products
{
    public record ProductHistoryResponse(Guid Id, string Name, string Type, int Quantity, double Value, DateTime TradeDate);
}