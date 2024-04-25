﻿using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Application.Profiles.Common;
using Xp.FinancialPortfolioManager.Application.Profiles.Queries.ListAdvisors;

namespace Xp.FinancialPortfolioManager.Application.Profiles.Queries.ListClients
{
    public class ListClientsQueryHandler(
        IClientsRepository clientsRepository,
        IUsersRepository usersRepository,
        ICurrentUserProvider currentUserProvider,
        IUnitOfWork unitOfWork)
            : IRequestHandler<ListClientsQuery, ErrorOr<List<ClientsQueryResult>>>
    {
        private readonly IClientsRepository _clientsRepository = clientsRepository;
        private readonly IUsersRepository _usersRepository = usersRepository;
        private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<List<ClientsQueryResult>>> Handle(ListClientsQuery query, CancellationToken cancellationToken)
        {                                
            var clientsList = new List<ClientsQueryResult>();
            var clients = await _clientsRepository.ListByAdvisorIdAsync(query.AdvisorId);

            if (clients is null)
                return Error.NotFound("Nenhum cliente encontrado para este assessor");

            foreach (var client in clients)
            {
                var user = await _usersRepository.GetByIdAsync(client.UserId);
                var clientResult = new ClientsQueryResult
                (
                    Name: user.FirstName + " " + user.LastName,
                    Email: user.Email,
                    ClientId: client.Id
                );

                clientsList.Add(clientResult);
            }

            return clientsList;
        }
    }
}