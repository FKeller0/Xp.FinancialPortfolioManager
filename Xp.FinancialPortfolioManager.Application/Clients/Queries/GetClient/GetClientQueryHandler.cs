using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Clients.Common;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Clients;

namespace Xp.FinancialPortfolioManager.Application.Clients.Queries.GetClient
{
    public class GetClientQueryHandler(
        IClientsRepository _clientsRepository,
        IUsersRepository _usersRepository,
        IIsValidUser _isValidUser)
            : IRequestHandler<GetClientQuery, ErrorOr<ClientQueryResult>>
    {        
        public async Task<ErrorOr<ClientQueryResult>> Handle(GetClientQuery query, CancellationToken cancellationToken)
        {
            var isValidUser = await _isValidUser.ValidateUserAsync(query.ClientId);

            if (isValidUser.IsError)
                return isValidUser.FirstError;

            if (await _clientsRepository.GetByIdAsync(query.ClientId) is not Client client)
                return Error.NotFound(description: "Cliente não encontrado");            

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