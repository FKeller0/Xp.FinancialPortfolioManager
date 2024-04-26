using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Clients.Common;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Advisors;
using Xp.FinancialPortfolioManager.Domain.Clients;

namespace Xp.FinancialPortfolioManager.Application.Clients.Queries.GetClient
{
    public class GetClientQueryHandler(
        IClientsRepository clientsRepository,
        IUsersRepository usersRepository,
        ICurrentUserProvider currentUserProvider,
        IAdvisorsRepository advisorsRepository)
            : IRequestHandler<GetClientQuery, ErrorOr<ClientQueryResult>>
    {
        private readonly IClientsRepository _clientsRepository = clientsRepository;
        private readonly IUsersRepository _usersRepository = usersRepository;
        private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;
        private readonly IAdvisorsRepository _advisorsRepository = advisorsRepository;

        public async Task<ErrorOr<ClientQueryResult>> Handle(GetClientQuery query, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();            

            if (await _clientsRepository.GetByIdAsync(query.ClientId) is not Client client)
                return Error.NotFound(description: "Cliente não encontrado");

            if (currentUser.Id != client.UserId) 
            {
                if (await _advisorsRepository.GetByUserIdAsync(currentUser.Id) is Advisor advisor) 
                {
                    if (client.AdvisorId != advisor.Id) 
                    {
                        return Error.Conflict(description: "O usuário não pode efetuar esta ação");
                    }
                }
            }

            var user = await _usersRepository.GetByIdAsync(client.UserId);
            var clientResult = new ClientQueryResult
            (
                User: user,
                Balance: client.Balance,
                ClientId: client.Id
            );

            return clientResult;
        }
    }
}