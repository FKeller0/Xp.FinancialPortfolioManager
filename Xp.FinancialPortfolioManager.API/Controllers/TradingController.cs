﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Xp.FinancialPortfolioManager.Application.Trading.Commands.BuyProduct;
using Xp.FinancialPortfolioManager.Application.Trading.Commands.SellProduct;
using Xp.FinancialPortfolioManager.Contracts.Trading;

namespace Xp.FinancialPortfolioManager.API.Controllers
{
    [Route("[controller]")]
    public class TradingController(ISender _mediator) : ApiController
    {
        [Authorize]
        [HttpPost("buyProduct")]
        [SwaggerOperation(Summary = "Permite que um Assessor ou Cliente logado comprem um produto")]
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
        [SwaggerOperation(Summary = "Permite que um Assessor ou Cliente logado vendam um produto do portfólio do cliente")]
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