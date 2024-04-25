namespace Xp.FinancialPortfolioManager.Application.Profiles.Common
{
    public record ClientsQueryResult(
        string Name,
        string Email,
        Guid ClientId);
}