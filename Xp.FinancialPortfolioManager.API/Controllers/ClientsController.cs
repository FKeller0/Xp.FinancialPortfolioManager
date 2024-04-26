using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xp.FinancialPortfolioManager.Application.Clients.Common;
using Xp.FinancialPortfolioManager.Application.Clients.Queries.GetClient;
using Xp.FinancialPortfolioManager.Contracts.Clients;

namespace Xp.FinancialPortfolioManager.API.Controllers
{
    [Route("[controller]")]
    public class ClientsController(ISender _mediator) : ApiController
    {
        [HttpGet("{clientId:guid}")]
        public async Task<IActionResult> GetClient(Guid clientId) 
        {
            var query = new GetClientQuery(clientId);

            var getClientResult = await _mediator.Send(query);

            return getClientResult.Match(
                clientResult => base.Ok(MapToClientResponse(clientResult)),
                Problem);
        }

        //[HttpGet("{productId:guid}")]
        //public async Task<IActionResult> GetProduct(Guid productId)
        //{
        //    var query = new GetProductQuery(productId);

        //    var getNoteResult = await _mediator.Send(query);

        //    return getNoteResult.Match(
        //        product => Ok(
        //            new ProductResponse(
        //                product.Id,
        //                product.Name,
        //                product.Description,
        //                product.ExpiresAt,
        //                product.CreatedAt)),
        //        Problem);
        //}

        private static ClientResponse MapToClientResponse(ClientQueryResult clientResult)
        {
            return new ClientResponse(
                clientResult.ClientId,
                clientResult.User.FirstName,
                clientResult.User.LastName,
                clientResult.User.Email,
                clientResult.Balance);
        }
    }
}