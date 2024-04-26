using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Products;

namespace Xp.FinancialPortfolioManager.Application.Products.Queries.GetProduct
{
    public class GetProductQueryHandler(
        IProductsRepository productsRepository)
            : IRequestHandler<GetProductQuery, ErrorOr<Product>>
    {
        private readonly IProductsRepository _productsRepository = productsRepository;                

        public async Task<ErrorOr<Product>> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {            
            if (await _productsRepository.GetByIdAsync(query.ProductId) is not Product product)
                return Error.NotFound(description: "Produto não encontrado");

            return product;
        }
    }
}