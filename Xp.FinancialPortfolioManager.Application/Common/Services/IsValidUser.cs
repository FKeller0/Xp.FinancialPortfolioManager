using ErrorOr;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Advisors;
using Xp.FinancialPortfolioManager.Domain.Clients;

namespace Xp.FinancialPortfolioManager.Application.Common.Services
{
    public class IsValidUser(ICurrentUserProvider currentUserProvider,
        IClientsRepository clientsRepository,
        IAdvisorsRepository advisorsRepository) : IIsValidUser
    {
        private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;
        private readonly IClientsRepository _clientsRepository = clientsRepository;
        private readonly IAdvisorsRepository _advisorsRepository = advisorsRepository;

        public async Task<ErrorOr<bool>> ValidateUserAsync(Guid clientId) 
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            if (await _clientsRepository.GetByIdAsync(clientId) is not Client client)
                return Error.NotFound(description: "Cliente não encontrado");

            if (currentUser.Roles.Contains("Admin"))
                return true;

            if (currentUser.Id != client.UserId)
            {
                if (await _advisorsRepository.GetByUserIdAsync(currentUser.Id) is Advisor advisor)
                {
                    if (client.AdvisorId != advisor.Id)
                        return Error.Conflict(description: "O usuário não pode efetuar esta ação");
                }                                      
            }

            return true;
        }
    }
}