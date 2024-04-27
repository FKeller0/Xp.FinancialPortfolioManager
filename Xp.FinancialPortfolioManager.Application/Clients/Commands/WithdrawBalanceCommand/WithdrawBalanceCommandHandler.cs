using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Clients;

namespace Xp.FinancialPortfolioManager.Application.Clients.Commands.WithdrawBalanceCommand
{
    public class WithdrawBalanceCommandHandler(
        IIsValidUser _isValidUser,
        IClientsRepository _clientsRepository,        
        IUnitOfWork _unitOfWork)
            : IRequestHandler<WithdrawBalanceCommand, ErrorOr<Success>>
    {        
        public async Task<ErrorOr<Success>> Handle(WithdrawBalanceCommand command, CancellationToken cancellationToken)
        {
            var isValidUser = await _isValidUser.ValidateUserAsync(command.ClientId);

            if (isValidUser.IsError)
                return isValidUser.FirstError;

            if (await _clientsRepository.GetByIdAsync(command.ClientId) is not Client client)
                return Error.NotFound(description: "Cliente não encontrado");            

            if (client.Balance < command.Balance) 
            {
                return Error.Conflict(description: "Saldo insuficiente para efetuar esta operação");
            }

            client.Balance -= command.Balance;

            await _clientsRepository.UpdateAsync(client);
            await _unitOfWork.CommitChangesAsync();

            return Result.Success;
        }
    }
}