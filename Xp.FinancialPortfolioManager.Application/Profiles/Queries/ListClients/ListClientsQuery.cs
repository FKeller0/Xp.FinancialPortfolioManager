﻿using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Authorization;
using Xp.FinancialPortfolioManager.Application.Profiles.Common;

namespace Xp.FinancialPortfolioManager.Application.Profiles.Queries.ListClients
{
    [Authorize(Roles = "Admin,Advisor")]
    public record ListClientsQuery(Guid AdvisorId) : IRequest<ErrorOr<List<ClientsQueryResult>>>;
}