using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Authentication.Common;

namespace Xp.FinancialPortfolioManager.Application.Authentication.Queries
{
    public record LoginQuery(
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}