using FluentValidation;

namespace Xp.FinancialPortfolioManager.Application.Products.Commands.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty();

            RuleFor(p => p.Description).NotEmpty();

            RuleFor(p => p.ExpiresAt).NotEmpty();
            
            RuleFor(x => x).Must(x => x.ExpiresAt == default || x.ExpiresAt >= DateTime.Now.Date)
                .WithMessage("A data de expiração do produto não pode ser inferior a data atual");
        }
    }
}