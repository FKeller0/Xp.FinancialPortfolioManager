using Microsoft.EntityFrameworkCore;
using Xp.FinancialPortfolioManager.Domain.ClientPortfolios;

namespace Xp.FinancialPortfolioManager.Infrastructure.ClientPortfolios
{
    public class ClientPortfolioConfiguration : IEntityTypeConfiguration<ClientPortfolio>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ClientPortfolio> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Property(p => p.ClientId);
            builder.Property(p => p.ProductId);
            builder.Property(p => p.ProductName);
            builder.Property(p => p.Quantity);
            builder.Property(p => p.BoughtFor);
            builder.Property(p => p.TotalPrice);
            builder.Property(p => p.BoughtAt);
        }
    }
}