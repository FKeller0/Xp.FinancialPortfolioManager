using FluentValidation;

namespace Xp.FinancialPortfolioManager.Application.Products.Commands.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty();

            RuleFor(p => p.Description).NotEmpty();                    
        }
    }
}