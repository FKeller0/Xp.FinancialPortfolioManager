using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Xp.FinancialPortfolioManager.Application.Profiles.Commands.CreateAdvisorProfile
{
    [Authorize(Roles = "Admin")]
    public record CreateAdvisorProfileCommand(Guid UserId) : IRequest<ErrorOr<Guid>>;
}