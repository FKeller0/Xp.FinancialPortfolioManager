using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Application.Profiles.Common;

namespace Xp.FinancialPortfolioManager.Application.Profiles.Queries.ListAdvisors
{
    public class ListAdvisorsQueryHandler(
        IAdvisorsRepository _advisorsRepository,
        IUsersRepository _usersRepository)
            : IRequestHandler<ListAdvisorsQuery, ErrorOr<List<AdvisorsQueryResult>>>
    {        
        public async Task<ErrorOr<List<AdvisorsQueryResult>>> Handle(ListAdvisorsQuery query, CancellationToken cancellationToken)
        {                    
            var advisorsList = new List<AdvisorsQueryResult>();
            var advisors = await _advisorsRepository.ListAdvisors();

            if (advisors is null)
                return Error.NotFound("Nenhum assessor encontrado");

            foreach (var advisor in advisors) 
            {
                var user = await _usersRepository.GetByIdAsync(advisor.UserId);
                var advisorResult = new AdvisorsQueryResult
                (
                    User: user,
                    AdvisorId: advisor.Id
                );

                advisorsList.Add(advisorResult);
            }

            return advisorsList;
        }
    }
}