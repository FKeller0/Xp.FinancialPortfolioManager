using Xp.FinancialPortfolioManager.Application.Common.Models;

namespace Xp.FinancialPortfolioManager.Application.Common.Interfaces
{
    public interface ICurrentUserProvider
    {
        CurrentUser GetCurrentUser();
    }
}