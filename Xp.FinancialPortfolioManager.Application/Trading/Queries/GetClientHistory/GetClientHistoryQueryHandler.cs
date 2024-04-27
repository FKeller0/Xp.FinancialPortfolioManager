using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.ClientsHistory;

namespace Xp.FinancialPortfolioManager.Application.Trading.Queries.GetClientHistory
{
    public class GetClientHistoryQueryHandler(
        IClientsHistoryRepository _clientsHistoryRepository,        
        IIsValidUser _isValidUser) 
            : IRequestHandler<GetClientHistoryQuery, ErrorOr<List<ClientHistory>>>
    {
        public async Task<ErrorOr<List<ClientHistory>>> Handle(GetClientHistoryQuery query, CancellationToken cancellationToken)
        {
            var isValidUser = await _isValidUser.ValidateUserAsync(query.ClientId);

            if (isValidUser.IsError) 
            {
                return isValidUser.FirstError;
            }

            var history = await _clientsHistoryRepository.ListClientHistory(query.ClientId);            

            return history;
        }
    }
}