using Xp.FinancialPortfolioManager.Domain.Common;

namespace Xp.FinancialPortfolioManager.Domain.Products
{
    public class Product : Entity
    {
        public string Name {  get; }
        public string Description { get; }        
        public DateTime CreatedAt { get; }
        public DateTime ExpiresAt { get; }

        public Product(
            string name,
            string description,            
            DateTime expiresAt,
            Guid? id = null)
            : base(id ?? Guid.NewGuid())
        {
            Name = name;
            Description = description;            
            CreatedAt = DateTime.UtcNow;
            ExpiresAt = expiresAt;
            Id = id ?? Guid.NewGuid();
        }

        public Product() { }
    }
}