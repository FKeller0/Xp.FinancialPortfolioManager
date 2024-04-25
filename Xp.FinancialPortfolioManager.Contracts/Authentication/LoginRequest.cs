namespace Xp.FinancialPortfolioManager.Contracts.Authentication
{
    public record LoginRequest(
        string Email,
        string Password);
}