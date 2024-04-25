using Xp.FinancialPortfolioManager.Domain.Users;

namespace Xp.FinancialPortfolioManager.Application.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token);
}