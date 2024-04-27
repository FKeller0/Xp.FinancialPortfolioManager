using Coravel;
using Microsoft.OpenApi.Models;
using Xp.FinancialPortfolioManager.API.Services;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;

namespace Xp.FinancialPortfolioManager.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddProblemDetails();
            services.AddHttpContextAccessor();
            services.AddScheduler();
            services.AddTransient<ExpiryJobInvocable>();
            
            services.Configure<SmtpSettings>(options => configuration.GetSection("SmtpSettings").Bind(options));            

            services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
            services.AddScoped<IMailerService, MailerService>();

            return services;
        }

        private static IServiceCollection AddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Xp.FinancialPortfolioManager", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            return services;
        }
    }
}