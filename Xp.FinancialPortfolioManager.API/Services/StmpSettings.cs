namespace Xp.FinancialPortfolioManager.API.Services
{
    public class SmtpSettings
    {
        public const string Section = "StmpSettings";

        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public string User { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}