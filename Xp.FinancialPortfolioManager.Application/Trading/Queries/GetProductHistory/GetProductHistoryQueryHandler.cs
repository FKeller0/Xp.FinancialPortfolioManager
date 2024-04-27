using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.ProductsHistory;

namespace Xp.FinancialPortfolioManager.Application.Trading.Queries.GetProductHistory
{
    public class GetProductHistoryQueryHandler(
        IProductHistoryRepository _productHistoryRepository)
            : IRequestHandler<GetProductHistoryQuery, ErrorOr<List<ProductHistory>>>
    {
        public async Task<ErrorOr<List<ProductHistory>>> Handle(GetProductHistoryQuery query, CancellationToken cancellationToken)
        {            
            var history = await _productHistoryRepository.ListProductHistory(query.ProductId);

            return history;
        }
    }
}