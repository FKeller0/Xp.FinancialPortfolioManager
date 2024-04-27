using Xp.FinancialPortfolioManager.API;
using Xp.FinancialPortfolioManager.Application;
using Xp.FinancialPortfolioManager.Infrastructure;
using Coravel;
using Xp.FinancialPortfolioManager.API.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation(builder.Configuration)
        .AddApplication()
        .AddInfrastructure(builder.Configuration);    
}

var app = builder.Build();

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<ExpiryJobInvocable>().EveryMinute().PreventOverlapping(nameof(ExpiryJobInvocable));
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();