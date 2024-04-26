using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Clients.Common;

namespace Xp.FinancialPortfolioManager.Application.Clients.Queries.GetClient
{
    public record GetClientQuery(Guid ClientId) : IRequest<ErrorOr<ClientQueryResult>>;
}