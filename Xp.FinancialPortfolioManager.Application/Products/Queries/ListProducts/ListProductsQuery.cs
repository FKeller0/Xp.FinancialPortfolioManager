using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Domain.Products;

namespace Xp.FinancialPortfolioManager.Application.Products.Queries.ListProducts
{
    public record ListProductsQuery() : IRequest<ErrorOr<List<Product>>>;        
}