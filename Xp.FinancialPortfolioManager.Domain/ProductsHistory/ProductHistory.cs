using Xp.FinancialPortfolioManager.Domain.Common;

namespace Xp.FinancialPortfolioManager.Domain.ProductsHistory
{
    public class ProductHistory : Entity
    {
        public Guid ProductId { get; }
        public string ProductName { get; } = null!;
        public int Quantity { get; set; }
        public double Value { get; }
        public string Type { get; } = null!;
        public DateTime TradeDate { get; }

        public ProductHistory(
            Guid productId,
            string productName,
            int quantity,
            double value,
            string type,
            Guid? id = null)
            : base(id ?? Guid.NewGuid())
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Value = value;            
            Type = type;
            TradeDate = DateTime.Now;
        }

        public ProductHistory() { }
    }
}