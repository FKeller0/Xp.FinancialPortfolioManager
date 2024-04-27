using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xp.FinancialPortfolioManager.Domain.ClientsHistory;

namespace Xp.FinancialPortfolioManager.Infrastructure.ClientsHistory
{
    public class ClientHistoryConfiguration : IEntityTypeConfiguration<ClientHistory>
    {
        public void Configure(EntityTypeBuilder<ClientHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.ClientId);
            builder.Property(x => x.ProductName);
            builder.Property(x => x.Quantity);
            builder.Property(x => x.Value);
            builder.Property(x => x.TotalPrice);
            builder.Property(x => x.Type);
            builder.Property(x => x.TradeDate);
        }
    }
}