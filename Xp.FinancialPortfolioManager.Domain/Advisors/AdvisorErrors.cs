using ErrorOr;

namespace Xp.FinancialPortfolioManager.Domain.Advisors
{
    public static class AdvisorErrors
    {
        public static readonly Error ClientIsAlreadyManagedByAdvisor = Error.Validation(
        "Client.AlreadyManagedByAdvisor",
        "Cliente já é gerenciado por este assessor.");
    }
}