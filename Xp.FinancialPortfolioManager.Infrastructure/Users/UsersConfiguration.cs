using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xp.FinancialPortfolioManager.Domain.Users;

namespace Xp.FinancialPortfolioManager.Infrastructure.Users
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName);

            builder.Property(u => u.LastName);

            builder.Property(u => u.Email);

            builder.Property(u => u.AdvisorId);

            builder.Property(u => u.ClientId);

            builder.Property("_passwordHash")
                .HasColumnName("PasswordHash");
        }
    }
}
