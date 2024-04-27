using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Xp.FinancialPortfolioManager.Application.Profiles.Commands.CreateAdvisorProfile;
using Xp.FinancialPortfolioManager.Application.Profiles.Commands.CreateClientProfile;
using Xp.FinancialPortfolioManager.Application.Profiles.Common;
using Xp.FinancialPortfolioManager.Application.Profiles.Queries.ListAdvisors;
using Xp.FinancialPortfolioManager.Application.Profiles.Queries.ListClients;
using Xp.FinancialPortfolioManager.Contracts.Profiles;

namespace Xp.FinancialPortfolioManager.API.Controllers
{
    [Route("[controller]")]
    public class ProfilesController(ISender _mediator) : ApiController
    {
        [SwaggerOperation(Summary = "Permite listar todos os Assessores disponíveis")]        
        [HttpGet("advisors")]
        public async Task<IActionResult> ListAdvisors()
        {
            var query = new ListAdvisorsQuery();

            var listAdvisorsResult = await _mediator.Send(query);

            return listAdvisorsResult.Match(
                advisorsResult => base.Ok(MapToAdvisorsResponse(advisorsResult)),
                Problem);
        }

        [HttpGet("clients")]
        [Authorize]
        [SwaggerOperation(Summary = "Permite que um Assessor logado liste todos seus clientes")]
        public async Task<IActionResult> ListClients(Guid advisorId)
        {
            var query = new ListClientsQuery(advisorId);

            var listClientsResult = await _mediator.Send(query);

            return listClientsResult.Match(
                clientsResult => base.Ok(MapToClientsResponse(clientsResult)),
                Problem);
        }

        [HttpPost("advisor")]
        [Authorize]
        [SwaggerOperation(Summary = "Permite que um Admin logado adicione um perfil de Assessor a um usuário")]
        public async Task<IActionResult> CreateAdvisorProfile(Guid userId)
        {
            var command = new CreateAdvisorProfileCommand(userId);

            var createProfileResult = await _mediator.Send(command);

            return createProfileResult.Match(
                id => Ok(new ProfileResponse(id)),
                Problem);
        }

        [HttpPost("client")]
        [Authorize]
        [SwaggerOperation(Summary = "Permite que um Admin ou Assessor logado adicione um perfil de Cliente a um usuário")]
        public async Task<IActionResult> CreateClientProfile(Guid userId, Guid advisorId, double? balance)
        {
            var command = new CreateClientProfileCommand(userId, advisorId, balance);

            var createProfileResult = await _mediator.Send(command);

            return createProfileResult.Match(
                id => Ok(new ProfileResponse(id)),
                Problem);
        }       

        private static List<AdvisorsQueryResponse> MapToAdvisorsResponse(List<AdvisorsQueryResult> authResult)
        {
            var list = new List<AdvisorsQueryResponse>();
            foreach(var auth in authResult) 
            {
                list.Add(new AdvisorsQueryResponse(
                    auth.AdvisorId,
                    auth.User.FirstName,
                    auth.User.LastName,
                    auth.User.Email));
            }
            return list;
        }

        private static List<ClientsQueryResponse> MapToClientsResponse(List<ClientsQueryResult> authResult)
        {
            var list = new List<ClientsQueryResponse>();
            foreach (var auth in authResult)
            {
                list.Add(new ClientsQueryResponse(
                    auth.ClientId,
                    auth.User.FirstName,
                    auth.User.LastName,
                    auth.User.Email));
            }
            return list;
        }
    }
}