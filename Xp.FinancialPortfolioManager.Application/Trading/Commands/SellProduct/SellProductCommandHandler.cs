using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.ClientPortfolios;
using Xp.FinancialPortfolioManager.Domain.Clients;
using Xp.FinancialPortfolioManager.Domain.ClientsHistory;
using Xp.FinancialPortfolioManager.Domain.ProductsHistory;

namespace Xp.FinancialPortfolioManager.Application.Trading.Commands.SellProduct
{
    public class SellProductCommandHandler(        
        IClientPorfolioRepository _clientPortfolioRepository,
        IClientsRepository _clientsRepository,
        IClientsHistoryRepository _clientHistoryRepository,
        IProductHistoryRepository _productHistoryRepository,
        IIsValidUser _isValidUser,
        IUnitOfWork _unitOfWork)
            : IRequestHandler<SellProductCommand, ErrorOr<Success>>
    {
        public async Task<ErrorOr<Success>> Handle(SellProductCommand command, CancellationToken cancellationToken)
        {
            var isValidUser = await _isValidUser.ValidateUserAsync(command.ClientId);

            if (isValidUser.IsError)
                return isValidUser.FirstError;

            if (await _clientsRepository.GetByIdAsync(command.ClientId) is not Client client)
                return Error.NotFound(description: "Cliente não encontrado");            

            if (await _clientPortfolioRepository.GetByIdAsync(command.ClientPortfolioProductId) is not ClientPortfolio product)
                return Error.NotFound(description: "Produto não encontrado no portfólio");

            if (product.Quantity < command.Quantity)
                return Error.Conflict(description: "Cliente não possui a quantidade de produtos informada para venda");
                
            client.Balance += command.Quantity * command.Value;

            if (product.Quantity - command.Quantity == 0)
            {
                await _clientPortfolioRepository.RemoveAsync(product);
            }
            else 
            {
                product.Quantity -= command.Quantity;
                await _clientPortfolioRepository.UpdateAsync(product);
            }

            var clientHistory = new ClientHistory(
                clientId: client.Id,
                productName: product.ProductName,
                quantity: command.Quantity,
                value: command.Value,
                type: "Venda");

            var productHistory = new ProductHistory(
                productId: product.Id,
                productName: product.ProductName,
                quantity: command.Quantity,
                value: command.Value,
                type: "Venda");

            await _clientsRepository.UpdateAsync(client);
            await _clientHistoryRepository.AddClientTradeHistoryAsync(clientHistory);
            await _productHistoryRepository.AddProductTradeHistoryAsync(productHistory);
            await _unitOfWork.CommitChangesAsync();

            return Result.Success;
        }
    }
}