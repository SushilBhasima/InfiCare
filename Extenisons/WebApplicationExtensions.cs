using InfiCare.Common.Interface;
using InfiCare.Common.Service;
using InfiCare.Infrastructure;
using Serilog;

namespace InfiCare.Extenisons;

public static class WebApplicationExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var config = builder.Configuration;

        builder.Host.UseSerilog((_, logger) => logger.ReadFrom.Configuration(config));

        // todo: using inMemory cache currently, but can be replaced with Redis or other distributed cache
        services.AddMemoryCache();

        // Add services to
        builder.Services.RefitConfigure();

        builder.Services.ConfigureInfrastructureService(config);

        builder.Services.AddControllersWithViews();
        // Add Razor Pages services
        builder.Services.AddRazorPages();

        builder.Services.AddTransient<ITransactionServices, TransactionService>();

        return builder;
    }
}
