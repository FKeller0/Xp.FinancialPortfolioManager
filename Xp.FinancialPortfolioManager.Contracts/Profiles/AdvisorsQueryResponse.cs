namespace Xp.FinancialPortfolioManager.Contracts.Profiles
{
    public record AdvisorsQueryResponse(
        Guid AdvisorId,
        string FirstName,
        string LastName,
        string Email);
}