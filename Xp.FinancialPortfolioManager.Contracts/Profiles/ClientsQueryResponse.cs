namespace Xp.FinancialPortfolioManager.Contracts.Profiles
{
    public record ClientsQueryResponse(
        Guid ClientId,
        string FirstName,
        string LastName,
        string Email);    
}