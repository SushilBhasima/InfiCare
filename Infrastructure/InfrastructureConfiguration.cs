using InfiCare.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InfiCare.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection ConfigureInfrastructureService(this IServiceCollection services, IConfiguration config)
    {
        ConfigureDatabase(services, config);
        return services;
    }

    private static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration config)
    {
        var connString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(opts =>
                        opts.UseSqlServer(connString,
                        builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
        );
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ "; // Add more allowed characters if needed
        })
            .AddEntityFrameworkStores<ApplicationDbContext>();
        return services;
    }
}
