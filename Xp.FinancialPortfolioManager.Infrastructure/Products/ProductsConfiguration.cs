using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xp.FinancialPortfolioManager.Domain.Products;

namespace Xp.FinancialPortfolioManager.Infrastructure.Products
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(n => n.Id)
                .ValueGeneratedNever();

            builder.Property(p => p.Name);
            builder.Property(p => p.Description);            
            builder.Property(p => p.CreatedAt);
            builder.Property(p => p.ExpiresAt);
        }
    }
}