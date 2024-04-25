using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Clients;

namespace Xp.FinancialPortfolioManager.Application.Profiles.Commands.CreateClientProfile
{
    public class CreateClientProfileCommandHandler(
        IUsersRepository _usersRepository,
        IAdvisorsRepository _advisorsRepository,
        IClientsRepository _clientsRepository,
        IUnitOfWork _unitOfWork)
            : IRequestHandler<CreateClientProfileCommand, ErrorOr<Guid>>
    {
        public async Task<ErrorOr<Guid>> Handle(CreateClientProfileCommand command, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetByIdAsync(command.UserId);

            if (user is null)
                return Error.NotFound(description: "Usuário não encontrado");            

            var advisor = await _advisorsRepository.GetByIdAsync(command.AdvisorId);

            if (advisor is null) 
                return Error.NotFound(description: "Assessor não encontrado");            

            var createClientProfileResult = user.CreateClientProfile();

            if (createClientProfileResult.IsError)
                return Error.Conflict(description: createClientProfileResult.FirstError.Description);

            var client = new Client(userId: user.Id, advisorId: advisor.Id, id: createClientProfileResult.Value, balance: command.Balance);

            await _usersRepository.UpdateAsync(user);
            await _clientsRepository.AddClientAsync(client);            
            await _unitOfWork.CommitChangesAsync();

            return createClientProfileResult;
        }
    }
}