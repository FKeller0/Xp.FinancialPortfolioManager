using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Application.Trading.Common;
using Xp.FinancialPortfolioManager.Domain.Clients;

namespace Xp.FinancialPortfolioManager.Application.Trading.Queries.GetClientPortfolio
{
    public class GetClientPortfolioQueryHandler(
        IClientPorfolioRepository _clientPortfolioRepository,
        IClientsRepository _clientsRepository,
        IIsValidUser _isValidUser)
            : IRequestHandler<GetClientPortfolioQuery, ErrorOr<List<ClientPortfolioQueryResult>>>
    {
        public async Task<ErrorOr<List<ClientPortfolioQueryResult>>> Handle(GetClientPortfolioQuery query, CancellationToken cancellationToken)
        {
            var isValidUser = await _isValidUser.ValidateUserAsync(query.ClientId);

            if (isValidUser.IsError)
                return isValidUser.FirstError;

            if (await _clientsRepository.GetByIdAsync(query.ClientId) is not Client client)
                return Error.NotFound(description: "Cliente não encontrado");

            var clientPortfolio = await _clientPortfolioRepository.ListClientPortfolio(client.Id);

            if (clientPortfolio.Count == 0)
                return Error.NotFound(description: "Cliente não possui produtos em seu portfólio");

            var portfolioResult = new List<ClientPortfolioQueryResult>();

            foreach (var item in clientPortfolio) 
            {
                portfolioResult.Add(new ClientPortfolioQueryResult
                (
                    PortfolioId: item.Id,
                    ProductId: item.ProductId,
                    ProductName: item.ProductName,
                    Quantity: item.Quantity,
                    BoughtFor: item.BoughtFor,
                    BoughtDate: item.BoughtAt
                ));
            }            

            return portfolioResult;
        }
    }
}