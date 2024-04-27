using Coravel.Invocable;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;

namespace Xp.FinancialPortfolioManager.API.Services
{
    public class ExpiryJobInvocable : IInvocable
    {
        private readonly IMailerService _mailerService;        
        public ExpiryJobInvocable(IMailerService catalogueService)
        {            
            _mailerService = catalogueService;
        }
        public async Task Invoke()
        {
            await _mailerService.SendAdvisorEmail();
        }
    }
}