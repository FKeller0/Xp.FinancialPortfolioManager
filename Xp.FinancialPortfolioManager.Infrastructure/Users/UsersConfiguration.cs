using Microsoft.AspNetCore.Identity;
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

            builder.Property(u => u.AdminId);

            builder.Property("_passwordHash")
                .HasColumnName("PasswordHash");

            builder.HasData(new User(
                firstName: "Sys",
                lastName: "Admin",
                email: "financialportfolio@xpfinancialportfolio.com",
                passwordHash: "$2a$11$fm.DCQn2.Cq7PlPfhwWDuO7pgEjGqs0pCf1P5Q24VejUqc.XxahLm",
                adminId: Guid.Parse("2150e333-8fdc-42a3-9474-1a3956d46de8"),
                id: Guid.Parse("1b41fae8-e02c-448b-944d-6169ebc26ddf")));
        }
    }
}
