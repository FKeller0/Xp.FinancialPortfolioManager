using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Xp.FinancialPortfolioManager.Application.Profiles.Commands.CreateClientProfile
{
    [Authorize(Roles = "Admin,Advisor")]
    public record CreateClientProfileCommand(Guid UserId, Guid AdvisorId, double? Balance) : IRequest<ErrorOr<Guid>>;
}