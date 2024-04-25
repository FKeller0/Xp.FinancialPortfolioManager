namespace Xp.FinancialPortfolioManager.Application.Common.Authorization
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AuthorizationAttribute : Attribute
    {
        public string? Permissions { get; set; }
        public string? Roles { get; set; }
    }
}