using Xp.FinancialPortfolioManager.Domain.Users;

namespace Xp.FinancialPortfolioManager.Application.Profiles.Common
{
    public record AdvisorsQueryResult(
        User User,
        Guid AdvisorId);    
}