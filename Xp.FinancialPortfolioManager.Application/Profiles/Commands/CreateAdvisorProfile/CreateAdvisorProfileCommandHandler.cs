using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Advisors;

namespace Xp.FinancialPortfolioManager.Application.Profiles.Commands.CreateAdvisorProfile
{
    public class CreateAdvisorProfileCommandHandler(
        IUsersRepository _usersRepository,
        IAdvisorsRepository _advisorsRepository,
        IUnitOfWork _unitOfWork)
            : IRequestHandler<CreateAdvisorProfileCommand, ErrorOr<Guid>>
    {
        public async Task<ErrorOr<Guid>> Handle(CreateAdvisorProfileCommand command, CancellationToken cancellationToken)
        {            
            var user = await _usersRepository.GetByIdAsync(command.UserId);

            if (user is null)
                return Error.NotFound(description: "Usuário não encontrado");            

            var advisor = await _advisorsRepository.GetByIdAsync(command.UserId);

            if (advisor is not null)
                return Error.Conflict(description: "Usuário já possui um perfil de assessor.");

            var createAdvisorProfileResult = user.CreateAdvisorProfile();

            if (createAdvisorProfileResult.IsError)
                return Error.Conflict(description: createAdvisorProfileResult.FirstError.Description);

            var createAdvisor = new Advisor(userId: user.Id, id: createAdvisorProfileResult.Value);

            await _usersRepository.UpdateAsync(user);
            await _advisorsRepository.AddAdvisorAsync(createAdvisor);
            await _unitOfWork.CommitChangesAsync();

            return createAdvisorProfileResult;
        }
    }
}