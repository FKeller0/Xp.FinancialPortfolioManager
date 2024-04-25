using ErrorOr;
using Xp.FinancialPortfolioManager.Domain.Common;
using Xp.FinancialPortfolioManager.Domain.Common.Interfaces;

namespace Xp.FinancialPortfolioManager.Domain.Users
{
    public class User : Entity
    {
        public string FirstName { get; } = null!;
        public string LastName { get; } = null!;
        public string Email { get; } = null!;
        public Guid? AdvisorId { get; private set; }
        public Guid? ClientId { get; private set; }
        public Guid? AdminId { get; private set; }

        private readonly string _passwordHash = null!;

        public User(
            string firstName,
            string lastName,
            string email,
            string passwordHash,
            Guid? advisorId = null,
            Guid? clientId = null,
            Guid? adminId = null,
            Guid? id = null)
            : base(id ?? Guid.NewGuid())
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            AdvisorId = advisorId;
            ClientId = clientId;
            AdminId = adminId;
            _passwordHash = passwordHash;
        }

        public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher) 
        {
            return passwordHasher.IsCorrectPassword(password, _passwordHash);
        }

        public ErrorOr<Guid> CreateAdvisorProfile()
        {
            if (AdvisorId is not null)
            {
                return Error.Conflict(description: "O usuário já possui um perfil de assessor.");
            }

            AdvisorId = Guid.NewGuid();

            return AdvisorId.Value;
        }

        public ErrorOr<Guid> CreateClientProfile()
        {
            if (ClientId is not null)
            {
                return Error.Conflict(description: "O usuário já possui um perfil de cliente.");
            }

            ClientId = Guid.NewGuid();

            return ClientId.Value;
        }

        public List<ProfileType> GetProfileTypes()
        {
            List<ProfileType> profileTypes = new();

            if (AdvisorId is not null)
            {
                profileTypes.Add(ProfileType.Advisor);
            }

            if (ClientId is not null)
            {
                profileTypes.Add(ProfileType.Client);
            }

            if (AdminId is not null) 
            {
                profileTypes.Add(ProfileType.Admin);
            }

            return profileTypes;
        }

        public User() { }

    }
}
