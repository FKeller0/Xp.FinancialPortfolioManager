using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Xp.FinancialPortfolioManager.Application.Clients.Commands.AddBalanceCommand;
using Xp.FinancialPortfolioManager.Application.Clients.Commands.WithdrawBalanceCommand;
using Xp.FinancialPortfolioManager.Application.Clients.Common;
using Xp.FinancialPortfolioManager.Application.Clients.Queries.GetClient;
using Xp.FinancialPortfolioManager.Application.Profiles.Common;
using Xp.FinancialPortfolioManager.Application.Trading.Common;
using Xp.FinancialPortfolioManager.Application.Trading.Queries.GetClientHistory;
using Xp.FinancialPortfolioManager.Application.Trading.Queries.GetClientPortfolio;
using Xp.FinancialPortfolioManager.Contracts.Clients;
using Xp.FinancialPortfolioManager.Contracts.Profiles;
using Xp.FinancialPortfolioManager.Domain.ClientsHistory;

namespace Xp.FinancialPortfolioManager.API.Controllers
{
    [Route("[controller]")]
    public class ClientsController(ISender _mediator) : ApiController
    {
        [Authorize]
        [HttpGet("{clientId:guid}")]
        [SwaggerOperation(Summary = "Permite que um Assessor ou Cliente logado consulte informações de um cliente")]
        public async Task<IActionResult> GetClient(Guid clientId) 
        {
            var query = new GetClientQuery(clientId);

            var getClientResult = await _mediator.Send(query);

            return getClientResult.Match(
                clientResult => base.Ok(MapToClientResponse(clientResult)),
                Problem);
        }

        [Authorize]
        [HttpGet("getClientHistory")]
        [SwaggerOperation(Summary = "Permite que um Assessor ou Cliente logado consulte informações de histórico de transações de um cliente")]
        public async Task<IActionResult> GetClientHistory(Guid clientId)
        {
            var query = new GetClientHistoryQuery(clientId);

            var getClientHistoryResult = await _mediator.Send(query);

            return getClientHistoryResult.Match(
                clientHistoryResult => base.Ok(MapToClientHistoryResponse(clientHistoryResult)), Problem);
        }

        [Authorize]
        [HttpGet("getClientPortfolio")]
        [SwaggerOperation(Summary = "Permite que um Assessor ou Cliente logado consulte informações do portfólio de produtos de um cliente")]
        public async Task<IActionResult> GetClientPortfolio(Guid clientId)
        {
            var query = new GetClientPortfolioQuery(clientId);

            var getClientPortfolioResult = await _mediator.Send(query);

            return getClientPortfolioResult.Match(
                clientPortfolioResult => base.Ok(MapToClientPortfolioResponse(clientPortfolioResult)), Problem);
        }

        [Authorize]
        [HttpPost("addBalance")]
        [SwaggerOperation(Summary = "Permite que um Assessor ou Cliente logado adicione saldo a sua conta")]
        public async Task<IActionResult> AddBalance(BalanceRequest request) 
        {
            var command = new AddBalanceCommand(request.ClientId, request.Balance);

            var getAddBalanceResult = await _mediator.Send(command);

            return getAddBalanceResult.Match(
                addBalanceResult => base.Ok(addBalanceResult), Problem);
        }

        [Authorize]
        [HttpPost("withdrawBalance")]
        [SwaggerOperation(Summary = "Permite que um Assessor ou Cliente logado saque um valor de sua conta")]
        public async Task<IActionResult> WithdrawBalance(BalanceRequest request)
        {
            var command = new WithdrawBalanceCommand(request.ClientId, request.Balance);

            var getAddBalanceResult = await _mediator.Send(command);

            return getAddBalanceResult.Match(
                addBalanceResult => base.Ok(addBalanceResult), Problem);
        }        

        private static ClientResponse MapToClientResponse(ClientQueryResult clientResult)
        {
            return new ClientResponse(
                clientResult.ClientId,
                clientResult.User.FirstName,
                clientResult.User.LastName,
                clientResult.User.Email,
                clientResult.Balance);
        }

        private static List<ClientHistoryResponse> MapToClientHistoryResponse(List<ClientHistory> histResult)
        {
            var list = new List<ClientHistoryResponse>();
            foreach (var hist in histResult)
            {
                list.Add(new ClientHistoryResponse(
                    hist.Id,
                    hist.ProductName,
                    hist.Type,
                    hist.Quantity,
                    hist.Value,
                    hist.TradeDate));
            }
            return list;
        }

        private static List<ClientPortfolioResponse> MapToClientPortfolioResponse(List<ClientPortfolioQueryResult> histResult)
        {
            var list = new List<ClientPortfolioResponse>();
            foreach (var hist in histResult)
            {
                list.Add(new ClientPortfolioResponse(
                    hist.PortfolioId,
                    hist.ProductId,
                    hist.ProductName,
                    hist.Quantity,
                    hist.BoughtFor,
                    hist.BoughtDate));
            }
            return list;
        }
    }
}