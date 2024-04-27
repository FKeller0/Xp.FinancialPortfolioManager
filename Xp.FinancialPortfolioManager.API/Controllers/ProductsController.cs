using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Xp.FinancialPortfolioManager.Application.Products.Commands.CreateProduct;
using Xp.FinancialPortfolioManager.Application.Products.Queries.GetProduct;
using Xp.FinancialPortfolioManager.Application.Products.Queries.ListProducts;
using Xp.FinancialPortfolioManager.Application.Trading.Queries.GetProductHistory;
using Xp.FinancialPortfolioManager.Contracts.Clients;
using Xp.FinancialPortfolioManager.Contracts.Products;
using Xp.FinancialPortfolioManager.Domain.ProductsHistory;

namespace Xp.FinancialPortfolioManager.API.Controllers
{
    [Route("[controller]")]
    public class ProductsController(ISender _mediator) : ApiController
    {
        [HttpGet("{productId:guid}")]
        [SwaggerOperation(Summary = "Permite obter informaçõesd de um produto específico")]
        public async Task<IActionResult> GetProduct(Guid productId)
        {
            var query = new GetProductQuery(productId);

            var getProductResult = await _mediator.Send(query);

            return getProductResult.Match(
                product => Ok(
                    new ProductResponse(
                        product.Id,
                        product.Name,
                        product.Description,                        
                        product.ExpiresAt,
                        product.CreatedAt)),
                Problem);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Permite listar todos os produtos disponíveis")]
        public async Task<IActionResult> ListProducts()
        {
            var query = new ListProductsQuery();

            var listProductResult = await _mediator.Send(query);

            return listProductResult.Match(
                product => Ok(product.ConvertAll(
                        product => new ProductResponse(
                            product.Id,
                            product.Name,
                            product.Description,                            
                            product.ExpiresAt,
                            product.CreatedAt))),
                    Problem);
        }
        
        [HttpGet("getProductHistory")]
        [SwaggerOperation(Summary = "Permite obter histórico de transações de um produto específico")]
        public async Task<IActionResult> GetProductHistory(Guid productId)
        {
            var query = new GetProductHistoryQuery(productId);

            var getClientHistoryResult = await _mediator.Send(query);

            return getClientHistoryResult.Match(
                clientHistoryResult => base.Ok(MapToProductHistoryResponse(clientHistoryResult)), Problem);
        }

        [HttpPost("product")]
        [Authorize]
        [SwaggerOperation(Summary = "Permite que um Admin ou Assessor criem um novo produto")]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        {
            var command = new CreateProductCommand(
                request.Name,
                request.Description,                
                request.ExpiresAt);

            var createProductResult = await _mediator.Send(command);

            return createProductResult.Match(
                product => CreatedAtAction(
                    nameof(GetProduct),
                    new { ProductId = product.Id },
                    new ProductResponse(
                            product.Id,
                            product.Name,
                            product.Description,                            
                            product.ExpiresAt,
                            product.CreatedAt)),
                    Problem);
        }

        private static List<ProductHistoryResponse> MapToProductHistoryResponse(List<ProductHistory> histResult)
        {
            var list = new List<ProductHistoryResponse>();
            foreach (var hist in histResult)
            {
                list.Add(new ProductHistoryResponse(
                    hist.Id,
                    hist.ProductName,
                    hist.Type,
                    hist.Quantity,
                    hist.Value,
                    hist.TradeDate));
            }
            return list;
        }
    }
}