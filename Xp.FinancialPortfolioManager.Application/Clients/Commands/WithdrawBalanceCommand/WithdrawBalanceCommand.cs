using ErrorOr;
using MediatR;

namespace Xp.FinancialPortfolioManager.Application.Clients.Commands.WithdrawBalanceCommand
{
    public record WithdrawBalanceCommand(Guid ClientId, double Balance) : IRequest<ErrorOr<Success>>;    
}