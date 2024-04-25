using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Authentication.Common;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Common.Interfaces;

namespace Xp.FinancialPortfolioManager.Application.Authentication.Queries
{
    public class LoginQueryHandler(
        IJwtTokenGenerator _jwtTokenGenerator,
        IPasswordHasher _passwordHasher,
        IUsersRepository _usersRepository)
        : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetByEmailAsync(query.Email);

            return user is null || !user.IsCorrectPasswordHash(query.Password, _passwordHasher)
                ? AuthenticationErrors.InvalidCredentials
                : new AuthenticationResult(user, _jwtTokenGenerator.GenerateToken(user));
        }
    }
}