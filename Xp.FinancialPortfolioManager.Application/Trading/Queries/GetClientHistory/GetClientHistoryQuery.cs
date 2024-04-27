using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Domain.ClientsHistory;

namespace Xp.FinancialPortfolioManager.Application.Trading.Queries.GetClientHistory
{
    public record GetClientHistoryQuery(Guid ClientId) : IRequest<ErrorOr<List<ClientHistory>>>;
}