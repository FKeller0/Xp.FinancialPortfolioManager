using ErrorOr;
using MediatR;

namespace Xp.FinancialPortfolioManager.Application.Clients.Commands.AddBalanceCommand
{    
    public record AddBalanceCommand(Guid ClientId, double Balance) : IRequest<ErrorOr<Success>>;    
}