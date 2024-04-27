using ErrorOr;

namespace Xp.FinancialPortfolioManager.Application.Common.Interfaces
{
    public interface IIsValidUser
    {
        Task<ErrorOr<bool>> ValidateUserAsync(Guid clientId);
    }
}