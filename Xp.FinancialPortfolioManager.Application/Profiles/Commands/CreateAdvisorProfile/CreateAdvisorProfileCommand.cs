using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Authorization;

namespace Xp.FinancialPortfolioManager.Application.Profiles.Commands.CreateAdvisorProfile
{
    [Authorize(Roles = "Admin")]
    public record CreateAdvisorProfileCommand(Guid UserId) : IRequest<ErrorOr<Guid>>;
}