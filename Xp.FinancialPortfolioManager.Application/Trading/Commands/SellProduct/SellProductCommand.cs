using ErrorOr;
using MediatR;

namespace Xp.FinancialPortfolioManager.Application.Trading.Commands.SellProduct
{
    public record SellProductCommand(Guid ClientPortfolioProductId, Guid ClientId, int Quantity, double Value) : IRequest<ErrorOr<Success>>;
}