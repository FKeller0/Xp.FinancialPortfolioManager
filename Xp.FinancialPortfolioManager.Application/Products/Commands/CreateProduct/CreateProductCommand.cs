using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Xp.FinancialPortfolioManager.Domain.Products;

namespace Xp.FinancialPortfolioManager.Application.Products.Commands.CreateProduct
{
    [Authorize(Roles = "Admin,Advisor")]
    public record CreateProductCommand(
        string Name,
        string Description,
        double Value,
        DateTime ExpiresAt) : IRequest<ErrorOr<Product>>;
}