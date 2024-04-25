namespace Xp.FinancialPortfolioManager.Application.Profiles.Common
{
    public record AdvisorsQueryResult(
        string Name,
        string Email,
        Guid AdvisorId);    
}