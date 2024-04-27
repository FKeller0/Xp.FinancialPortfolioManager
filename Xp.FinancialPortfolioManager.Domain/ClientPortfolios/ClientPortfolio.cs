using Xp.FinancialPortfolioManager.Domain.Common;

namespace Xp.FinancialPortfolioManager.Domain.ClientPortfolios
{
    public class ClientPortfolio : Entity
    {
        public Guid ClientId { get; }
        public Guid ProductId { get; }
        public string ProductName { get; } = null!;
        public int Quantity { get; set; }
        public double BoughtFor { get; }
        public double TotalPrice { get; }
        public DateTime BoughtAt { get; }

        public ClientPortfolio(
            Guid clientId,
            Guid productId,
            string productName,
            int quantity,
            double boughtFor,
            Guid? id = null)
            : base(id ?? Guid.NewGuid())
        {
            ClientId = clientId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            BoughtFor = boughtFor;
            TotalPrice = quantity * boughtFor;
            BoughtAt = DateTime.Now;
        }

        public ClientPortfolio() { }
    }
}