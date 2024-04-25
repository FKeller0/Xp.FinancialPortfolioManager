using ErrorOr;
using MediatR;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Products;

namespace Xp.FinancialPortfolioManager.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler(
        IProductsRepository _productsRepository,        
        IUnitOfWork _unitOfWork)
            : IRequestHandler<CreateProductCommand, ErrorOr<Product>>
    {
        public async Task<ErrorOr<Product>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var productHist = await _productsRepository.GetByNameAsync(command.Name);

            if (productHist is not null)
                return Error.Conflict(description: "Produto já existe");            

            var product = new Product(
                command.Name,
                command.Description,
                command.Value,
                command.ExpiresAt);

            await _productsRepository.AddProductAsync(product);
            await _unitOfWork.CommitChangesAsync();

            return product;
        }
    }
}