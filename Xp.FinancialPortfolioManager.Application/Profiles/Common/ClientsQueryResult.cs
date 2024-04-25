using Xp.FinancialPortfolioManager.Domain.Users;

namespace Xp.FinancialPortfolioManager.Application.Profiles.Common
{
    public record ClientsQueryResult(
        User User,
        Guid ClientId);
}