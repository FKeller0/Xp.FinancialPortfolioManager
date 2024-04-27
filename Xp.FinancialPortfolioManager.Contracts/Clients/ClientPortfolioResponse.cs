namespace Xp.FinancialPortfolioManager.Contracts.Clients
{
    public record ClientPortfolioResponse(Guid PortfolioId, Guid ProductId, string ProductName, int Quantity, double BoughtFor, DateTime BoughtDate);
}