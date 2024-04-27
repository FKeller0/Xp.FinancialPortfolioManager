using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Xp.FinancialPortfolioManager.Domain.ProductsHistory;

namespace Xp.FinancialPortfolioManager.Infrastructure.ProductsHistory
{
    public class ProductHistoryConfiguration : IEntityTypeConfiguration<ProductHistory>
    {
        public void Configure(EntityTypeBuilder<ProductHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.ProductId);
            builder.Property(x => x.ProductName);
            builder.Property(x => x.Quantity);
            builder.Property(x => x.Value);            
            builder.Property(x => x.Type);
            builder.Property(x => x.TradeDate);
        }
    }
}