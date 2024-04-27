using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Domain.ProductsHistory;

namespace Xp.FinancialPortfolioManager.Application.Trading.Queries.GetProductHistory
{
    public record GetProductHistoryQuery(Guid ProductId) : IRequest<ErrorOr<List<ProductHistory>>>;
}