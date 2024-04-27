using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Xp.FinancialPortfolioManager.Application.Common.Behaviors;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Application.Common.Services;

namespace Xp.FinancialPortfolioManager.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
                
                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
                options.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            });            

            services.AddScoped<IIsValidUser, IsValidUser>();            

            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

            return services;
        }
    }
}
