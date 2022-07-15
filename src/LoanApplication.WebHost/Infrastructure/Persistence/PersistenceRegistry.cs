using Microsoft.EntityFrameworkCore;

namespace LoanApplication.WebHost.Infrastructure.Persistence;

public static class PersistenceRegistry
{
    public static WebApplicationBuilder AddCustomPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LoanDbContext>(options =>
            options.UseSqlServer(
                "name=ConnectionStrings:DefaultConnection",
                o =>
                {
                    o.EnableRetryOnFailure();
                    o.MigrationsAssembly(typeof(LoanDbContext).Assembly.FullName);
                }));

        builder.Services.AddHostedService<PersistenceInitializer>();

        return builder;
    }
}
