using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Profiles.Common;

namespace Xp.FinancialPortfolioManager.Application.Profiles.Queries.ListClients
{
    public record ListClientsQuery(Guid AdvisorId) : IRequest<ErrorOr<List<ClientsQueryResult>>>;
}