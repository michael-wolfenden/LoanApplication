using LoanApplication.WebHost.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoanApplication.WebHost.Infrastructure.Persistence;

public class PersistenceInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public PersistenceInitializer(IServiceProvider serviceProvider) =>
        _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        await using var loanDbContext = scope.ServiceProvider.GetRequiredService<LoanDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<PersistenceInitializer>>();

        logger.LogInformation("Creating the database...");

        await loanDbContext.Database.EnsureDeletedAsync(cancellationToken);
        await loanDbContext.Database.MigrateAsync(cancellationToken);

        var admin = new Operator(
            Login.Of("admin").Value,
            Password.Of("admin").Value,
            Name.Of("admin", "admin").Value,
            new MonetaryAmount(1_000_000M)
        );

        await loanDbContext.Operators.AddAsync(admin, cancellationToken);
        await loanDbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Database created successfully");
    }

    public Task StopAsync(CancellationToken cancellationToken) =>
        Task.CompletedTask;
}
