using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Profiles.Common;

namespace Xp.FinancialPortfolioManager.Application.Profiles.Queries.ListAdvisors
{
    public record ListAdvisorsQuery() : IRequest<ErrorOr<List<AdvisorsQueryResult>>>;
}