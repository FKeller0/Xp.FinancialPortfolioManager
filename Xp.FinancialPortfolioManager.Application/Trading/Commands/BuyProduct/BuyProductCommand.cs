using ErrorOr;
using MediatR;

namespace Xp.FinancialPortfolioManager.Application.Trading.Commands.BuyProduct
{
    public record BuyProductCommand(Guid ProductId, Guid ClientId, int Quantity, double Value) : IRequest<ErrorOr<Success>>;
}