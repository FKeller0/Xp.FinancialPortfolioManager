﻿using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Authentication.Common;

namespace Xp.FinancialPortfolioManager.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}