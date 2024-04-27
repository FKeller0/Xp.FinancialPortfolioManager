namespace Xp.FinancialPortfolioManager.Application.Trading.Common
{
    public record ClientPortfolioQueryResult(Guid PortfolioId, Guid ProductId, string ProductName, int Quantity, double BoughtFor, DateTime BoughtDate);
}