using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.ClientPortfolios;
using Xp.FinancialPortfolioManager.Domain.Clients;
using Xp.FinancialPortfolioManager.Domain.ClientsHistory;
using Xp.FinancialPortfolioManager.Domain.Products;
using Xp.FinancialPortfolioManager.Domain.ProductsHistory;

namespace Xp.FinancialPortfolioManager.Application.Trading.Commands.BuyProduct
{
    public class BuyProductCommandHandler(
        IProductsRepository _productsRepository,
        IClientPorfolioRepository _clientPortfolioRepository,
        IClientsRepository _clientsRepository,
        IClientsHistoryRepository _clientsHistoryRepository,
        IProductHistoryRepository _productHistoryRepository,
        IIsValidUser _isValidUser,
        IUnitOfWork _unitOfWork) 
            : IRequestHandler<BuyProductCommand, ErrorOr<Success>>
    {
        public async Task<ErrorOr<Success>> Handle(BuyProductCommand command, CancellationToken cancellationToken)
        {
            var isValidUser = await _isValidUser.ValidateUserAsync(command.ClientId);

            if (isValidUser.IsError)
                return isValidUser.FirstError;

            if (await _clientsRepository.GetByIdAsync(command.ClientId) is not Client client)
                return Error.NotFound(description: "Cliente não encontrado");           

            if (await _productsRepository.GetByIdAsync(command.ProductId) is not Product product)
                return Error.NotFound(description: "Cliente não encontrado");

            if (client.Balance < (command.Quantity * command.Value))
                return Error.Conflict(description: "Saldo insuficiente para efetuar esta operação");

            if (product.ExpiresAt.Date < DateTime.Now.Date)
                return Error.Conflict(description: "Produto expirado");

            var clientProduct = new ClientPortfolio(
                clientId: client.Id,
                productId: product.Id,
                productName: product.Name,
                quantity: command.Quantity,
                boughtFor: command.Value);

            var clientHistory = new ClientHistory(
                clientId: client.Id,
                productName: product.Name,
                quantity: command.Quantity,
                value: command.Value,
                type: "Compra");

            var productHistory = new ProductHistory(
                productId: product.Id,
                productName: product.Name,
                quantity: command.Quantity,
                value: command.Value,
                type: "Compra");

            client.Balance -= command.Quantity * command.Value;            

            await _clientPortfolioRepository.AddProductToPortfolio(clientProduct);
            await _clientsHistoryRepository.AddClientTradeHistoryAsync(clientHistory);
            await _productHistoryRepository.AddProductTradeHistoryAsync(productHistory);
            await _clientsRepository.UpdateAsync(client);
            await _unitOfWork.CommitChangesAsync();

            return Result.Success;
        }
    }
}