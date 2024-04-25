using Xp.FinancialPortfolioManager.Domain.Advisors;

namespace Xp.FinancialPortfolioManager.Application.Common.Interfaces
{
    public interface IAdvisorsRepository
    {
        Task AddAdvisorAsync(Advisor advisor);
        Task<Advisor?> GetByIdAsync(Guid advisorId);
        Task UpdateAsync(Advisor advisor);
    }
}