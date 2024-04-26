using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Application.Profiles.Common;
using Xp.FinancialPortfolioManager.Domain.Products;

namespace Xp.FinancialPortfolioManager.Application.Products.Queries.ListProducts
{
    public class ListProductsQueryHandler(IProductsRepository productsRepository) : IRequestHandler<ListProductsQuery, ErrorOr<List<Product>>>
    {
        private readonly IProductsRepository _productsRepository = productsRepository;

        public async Task<ErrorOr<List<Product>>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {            
            var products = await _productsRepository.ListProducts();

            if (products is null)
                return Error.NotFound("Nenhum produto encontrado");            

            return products;
        }
    }
}