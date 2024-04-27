using Xp.FinancialPortfolioManager.Domain.Common;

namespace Xp.FinancialPortfolioManager.Domain.ClientsHistory
{
    public class ClientHistory : Entity
    {
        public Guid ClientId { get; }
        public string ProductName { get; } = null!;
        public int Quantity { get; set; }
        public double Value { get; }
        public double TotalPrice { get; }
        public string Type { get; } = null!;
        public DateTime TradeDate { get; }

        public ClientHistory(
            Guid clientId,
            string productName,
            int quantity,
            double value,
            string type,
            Guid? id = null)
            : base(id ?? Guid.NewGuid())
        {
            ClientId = clientId;
            ProductName = productName;
            Quantity = quantity;
            Value = value;
            TotalPrice = quantity * value;
            Type = type;
            TradeDate = DateTime.Now;
        }

        public ClientHistory() { }
    }
}