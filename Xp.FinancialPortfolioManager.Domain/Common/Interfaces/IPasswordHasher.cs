using ErrorOr;

namespace Xp.FinancialPortfolioManager.Domain.Common.Interfaces
{
    public interface IPasswordHasher
    {
        public ErrorOr<string> HashPassword(string password);
        bool IsCorrectPassword(string password, string hash);
    }
}