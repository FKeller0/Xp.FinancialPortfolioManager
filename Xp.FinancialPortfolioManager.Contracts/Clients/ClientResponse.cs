using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xp.FinancialPortfolioManager.Contracts.Clients
{
    public record ClientResponse(
        Guid ClientId,
        string FirstName,
        string LastName,
        string Email,
        double? Balance);
}