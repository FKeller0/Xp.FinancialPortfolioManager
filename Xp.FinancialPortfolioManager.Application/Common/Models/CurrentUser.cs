namespace Xp.FinancialPortfolioManager.Application.Common.Models
{
    public record CurrentUser(
        Guid Id,
        IReadOnlyList<string> Permissions,
        IReadOnlyList<string> Roles);
}