using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xp.FinancialPortfolioManager.Application.Authentication.Commands.Register;
using Xp.FinancialPortfolioManager.Application.Authentication.Common;
using Xp.FinancialPortfolioManager.Application.Authentication.Queries;
using Xp.FinancialPortfolioManager.Contracts.Authentication;

namespace Xp.FinancialPortfolioManager.API.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthenticationController(ISender _mediator) : ApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            var authResult = await _mediator.Send(command);

            return authResult.Match(
                authResult => base.Ok(MapToAuthResponse(authResult)),
                Problem);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginQuery(request.Email, request.Password);

            var authResult = await _mediator.Send(query);

            if (authResult.IsError && authResult.FirstError == AuthenticationErrors.InvalidCredentials)
            {
                return Problem(
                    detail: authResult.FirstError.Description,
                    statusCode: StatusCodes.Status401Unauthorized);
            }

            return authResult.Match(
                authResult => Ok(MapToAuthResponse(authResult)),
                Problem);
        }

        private static AuthenticationResponse MapToAuthResponse(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token);
        }
    }
}