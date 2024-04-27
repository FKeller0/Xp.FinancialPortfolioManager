using MimeKit;
using System.Net;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using System.Runtime;

namespace Xp.FinancialPortfolioManager.API.Services
{
    public class MailerService(
        IProductsRepository _productsRepository,
        IAdvisorsRepository _advisorsRepository,
        IUsersRepository _usersRepository,
        IOptions<SmtpSettings> settings) : IMailerService
    {        
        public async Task SendAdvisorEmail()
        {            
            var products = await _productsRepository.ListProducts();

            var productList = products.Where(x => x.ExpiresAt.Date == DateTime.Now.Date).ToList();
            var advisors = await _advisorsRepository.ListAdvisors();

            var email = new MimeMessage();

            var nomeProduto = "";

            if (products.Count > 0)
            {
                foreach (var product in productList)
                {
                    nomeProduto += product.Name + "<br>";
                }
            }

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<b>Assessor, os produtos a seguir expirarão em breve:</b><br>{nomeProduto}"
            };

            var client = new SmtpClient(settings.Value.Host, settings.Value.Port)
            {
                Credentials = new NetworkCredential(settings.Value.User, settings.Value.Password),
                EnableSsl = true
            };

            foreach (var advisor in advisors)
            {
                var user = await _usersRepository.GetByIdAsync(advisor.UserId);
                client.Send("financialportfoliomanager@email.com", user.Email, "Produtos Expirantes", email.Body.ToString());
                await Task.Delay(10000);
            }
        }
    }
}