using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Domain.Products;

namespace Xp.FinancialPortfolioManager.Application.Products.Queries.GetProduct
{
    public record GetProductQuery(Guid ProductId) : IRequest<ErrorOr<Product>>;
}