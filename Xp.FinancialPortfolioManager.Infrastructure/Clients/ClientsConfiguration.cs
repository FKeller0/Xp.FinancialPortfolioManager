using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Xp.FinancialPortfolioManager.Domain.Clients;

namespace Xp.FinancialPortfolioManager.Infrastructure.Clients
{
    public class ClientsConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId);

            builder.Property(x => x.AdvisorId);

            builder.HasOne(x => x.Advisor)
                .WithMany(x => x.Clients)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.AdvisorId);

            builder.Property(x => x.Balance);
        }
    }
}