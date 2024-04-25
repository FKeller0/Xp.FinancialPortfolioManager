using Ardalis.SmartEnum;

namespace Xp.FinancialPortfolioManager.Domain.Users
{
    public class ProfileType(string name, int value) : SmartEnum<ProfileType>(name, value)
    {
        public static readonly ProfileType Advisor = new(nameof(Advisor), 0);
        public static readonly ProfileType Client = new(nameof(Client), 1);
    }
}