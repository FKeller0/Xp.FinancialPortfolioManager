using Xp.FinancialPortfolioManager.Domain.Common;

namespace Xp.FinancialPortfolioManager.Domain.Admin
{
    public class Admin : Entity
    {
        public Guid UserId { get; }

        public Admin(
            Guid userId,            
            Guid? id = null)
                : base(id ?? Guid.NewGuid())
        {
            UserId = userId;
        }

        private Admin() { }
    }
}