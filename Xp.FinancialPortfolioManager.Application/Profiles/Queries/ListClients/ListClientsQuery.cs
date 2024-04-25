using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Xp.FinancialPortfolioManager.Application.Profiles.Common;

namespace Xp.FinancialPortfolioManager.Application.Profiles.Queries.ListClients
{
    [Authorize(Roles = "Admin,Advisor")]
    public record ListClientsQuery(Guid AdvisorId) : IRequest<ErrorOr<List<ClientsQueryResult>>>;
}