using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Clients;

namespace Xp.FinancialPortfolioManager.Application.Clients.Commands.AddBalanceCommand
{
    public class AddBalanceCommandHandler(
        IIsValidUser _isValidUser,
        IClientsRepository _clientsRepository,        
        IUnitOfWork _unitOfWork)
            : IRequestHandler<AddBalanceCommand, ErrorOr<Success>>
    {        
        public async Task<ErrorOr<Success>> Handle(AddBalanceCommand command, CancellationToken cancellationToken)
        {
            var isValidUser = await _isValidUser.ValidateUserAsync(command.ClientId);

            if (isValidUser.IsError)
                return isValidUser.FirstError;

            if (await _clientsRepository.GetByIdAsync(command.ClientId) is not Client client)
                return Error.NotFound(description: "Cliente não encontrado");            
            
            client.Balance += command.Balance;

            await _clientsRepository.UpdateAsync(client);
            await _unitOfWork.CommitChangesAsync();

            return Result.Success;
        }
    }
}