using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Advisors;

namespace Xp.FinancialPortfolioManager.Application.Profiles.Commands.CreateAdvisorProfile
{
    public class CreateAdvisorProfileCommandHandler(
        IUsersRepository _usersRepository,
        IAdvisorsRepository _advisorsRepository,
        IUnitOfWork _unitOfWork,
        ICurrentUserProvider _currentUserProvider)
            : IRequestHandler<CreateAdvisorProfileCommand, ErrorOr<Guid>>
    {
        public async Task<ErrorOr<Guid>> Handle(CreateAdvisorProfileCommand command, CancellationToken cancellationToken)
        {            
            var user = await _usersRepository.GetByIdAsync(command.UserId);

            if (user is null)
            {
                return Error.NotFound(description: "Usuário não encontrado");
            }

            var createAdvisorProfileResult = user.CreateAdvisorProfile();
            var advisor = new Advisor(userId: user.Id, id: createAdvisorProfileResult.Value);

            await _usersRepository.UpdateAsync(user);
            await _advisorsRepository.AddAdvisorAsync(advisor);
            await _unitOfWork.CommitChangesAsync();

            return createAdvisorProfileResult;
        }
    }
}