using Xp.FinancialPortfolioManager.Domain.Advisors;
using Xp.FinancialPortfolioManager.Domain.Common;

namespace Xp.FinancialPortfolioManager.Domain.Clients
{
    public class Client : Entity
    {
        public Guid UserId { get; }
        public Guid AdvisorId { get; }
        public double? Balance { get; set; }
        public Advisor Advisor { get; set; } = null!;

        public Client(
            Guid userId,
            Guid advisorId,
            double? balance = null,
            Guid? id = null)
                : base(id ?? Guid.NewGuid())
        {
            UserId = userId;
            AdvisorId = advisorId;
            Balance = balance;
        }

        private Client() { }
    }
}