using Xp.FinancialPortfolioManager.Domain.Users;

namespace Xp.FinancialPortfolioManager.Application.Clients.Common
{
    public record ClientQueryResult(
        User User,
        double? Balance,
        Guid ClientId);
}