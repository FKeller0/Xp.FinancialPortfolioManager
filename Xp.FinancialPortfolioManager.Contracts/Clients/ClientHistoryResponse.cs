namespace Xp.FinancialPortfolioManager.Contracts.Clients
{
    public record ClientHistoryResponse(Guid Id, string Name, string Type, int Quantity, double Value, DateTime TradeDate);
}