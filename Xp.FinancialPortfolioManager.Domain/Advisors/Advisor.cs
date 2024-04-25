using ErrorOr;
using Xp.FinancialPortfolioManager.Domain.Clients;
using Xp.FinancialPortfolioManager.Domain.Common;

namespace Xp.FinancialPortfolioManager.Domain.Advisors
{
    public class Advisor : Entity
    {
        public Guid UserId { get; }
        public ICollection<Client> Clients { get; } = new List<Client>();        

        public Advisor(
            Guid userId,
            Guid? id = null)
                : base(id ?? Guid.NewGuid())
            {
                UserId = userId;                
            }

        private Advisor() { }

        public ErrorOr<Success> AddClient(Client client)
        {
            if (Clients.FirstOrDefault(client => client.Id == Id) is not null) 
            {
                return AdvisorErrors.ClientIsAlreadyManagedByAdvisor;
            }

            Clients.Add(client);

            return Result.Success;
        }
    }
}