using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Application.Profiles.Common;

namespace Xp.FinancialPortfolioManager.Application.Profiles.Queries.ListClients
{
    public class ListClientsQueryHandler(
        IClientsRepository _clientsRepository,
        IAdvisorsRepository _advisorsRepository,
        IUsersRepository _usersRepository,
        ICurrentUserProvider _currentUserProvider)
            : IRequestHandler<ListClientsQuery, ErrorOr<List<ClientsQueryResult>>>
    {        
        public async Task<ErrorOr<List<ClientsQueryResult>>> Handle(ListClientsQuery query, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            //TODO - Refatorar isso aqui
            if (!currentUser.Roles.Contains("Admin")) 
            {
                var advisor = await _advisorsRepository.GetByUserIdAsync(currentUser.Id);

                if (advisor is not null) 
                {
                    if (advisor.Id != query.AdvisorId)
                    {
                        return Error.Conflict("Essa ação não é permitida por este usuário.");
                    }
                }                
            }

            var clientsList = new List<ClientsQueryResult>();
            var clients = await _clientsRepository.ListByAdvisorIdAsync(query.AdvisorId);

            if (clients is null)
                return Error.NotFound("Nenhum cliente encontrado para este assessor");

            foreach (var client in clients)
            {
                var user = await _usersRepository.GetByIdAsync(client.UserId);
                var clientResult = new ClientsQueryResult
                (
                    User: user,
                    ClientId: client.Id
                );

                clientsList.Add(clientResult);
            }

            return clientsList;
        }
    }
}