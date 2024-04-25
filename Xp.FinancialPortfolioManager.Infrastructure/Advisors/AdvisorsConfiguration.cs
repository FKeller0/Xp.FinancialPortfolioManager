using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xp.FinancialPortfolioManager.Domain.Advisors;

namespace Xp.FinancialPortfolioManager.Infrastructure.Advisors
{
    public class AdvisorsConfiguration : IEntityTypeConfiguration<Advisor>
    {
        public void Configure(EntityTypeBuilder<Advisor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.UserId);

        }
    }
}