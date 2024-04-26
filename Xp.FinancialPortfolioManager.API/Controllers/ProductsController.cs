using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xp.FinancialPortfolioManager.Application.Products.Commands.CreateProduct;
using Xp.FinancialPortfolioManager.Application.Products.Queries.GetProduct;
using Xp.FinancialPortfolioManager.Application.Products.Queries.ListProducts;
using Xp.FinancialPortfolioManager.Contracts.Products;

namespace Xp.FinancialPortfolioManager.API.Controllers
{
    [Route("[controller]")]
    public class ProductsController(ISender _mediator) : ApiController
    {
        [HttpPost("product")]
        [Authorize]
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

        [HttpGet("{productId:guid}")]
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
        public async Task<IActionResult> ListNotes()
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
    }
}