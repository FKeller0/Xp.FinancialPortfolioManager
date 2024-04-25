using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xp.FinancialPortfolioManager.Domain.Admin;

namespace Xp.FinancialPortfolioManager.Infrastructure.Admins
{
    public class AdminsConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasData(new Admin(
                userId: Guid.Parse("1b41fae8-e02c-448b-944d-6169ebc26ddf"),
                id: Guid.Parse("2150e333-8fdc-42a3-9474-1a3956d46de8")));
        }
    }
}