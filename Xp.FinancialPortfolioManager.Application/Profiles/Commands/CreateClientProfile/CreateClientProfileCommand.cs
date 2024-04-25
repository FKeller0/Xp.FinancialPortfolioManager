using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Authorization;


namespace Xp.FinancialPortfolioManager.Application.Profiles.Commands.CreateClientProfile
{
    [Authorize(Roles = "Admin,Advisor")]
    public record CreateClientProfileCommand(Guid UserId, Guid AdvisorId, double? Balance) : IRequest<ErrorOr<Guid>>;
}