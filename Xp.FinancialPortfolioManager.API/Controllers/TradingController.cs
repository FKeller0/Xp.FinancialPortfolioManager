using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xp.FinancialPortfolioManager.Application.Trading.Commands.BuyProduct;
using Xp.FinancialPortfolioManager.Application.Trading.Commands.SellProduct;
using Xp.FinancialPortfolioManager.Application.Trading.Queries.GetClientHistory;
using Xp.FinancialPortfolioManager.Contracts.Trading;

namespace Xp.FinancialPortfolioManager.API.Controllers
{
    [Route("[controller]")]
    public class TradingController(ISender _mediator) : ApiController
    {
        [Authorize]
        [HttpPost("buyProduct")]
        public async Task<IActionResult> BuyProduct(BuyProductRequest request) 
        {
            var command = new BuyProductCommand(
                request.ProductId,
                request.ClientId,
                request.Quantity,
                request.Value);

            var getAddBalanceResult = await _mediator.Send(command);

            return getAddBalanceResult.Match(
                addBalanceResult => base.Ok(addBalanceResult), Problem);
        }

        [Authorize]
        [HttpPost("sellProduct")]
        public async Task<IActionResult> SellProduct(SellProductRequest request)
        {
            var command = new SellProductCommand(
                request.ClientPortfolioId,
                request.ClientId,
                request.Quantity,
                request.Value);

            var getAddBalanceResult = await _mediator.Send(command);

            return getAddBalanceResult.Match(
                addBalanceResult => base.Ok(addBalanceResult), Problem);
        }        
    }
}