using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Trading.Common;

namespace Xp.FinancialPortfolioManager.Application.Trading.Queries.GetClientPortfolio
{
    public record GetClientPortfolioQuery(Guid ClientId) : IRequest<ErrorOr<List<ClientPortfolioQueryResult>>>;
}