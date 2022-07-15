using System.Reflection;
using LoanApplication.WebHost.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoanApplication.WebHost.Infrastructure.Persistence;

public class LoanDbContext : DbContext
{
    public LoanDbContext(DbContextOptions<LoanDbContext> options)
        : base(options)
    {
    }

    public DbSet<Domain.LoanApplication> LoanApplications => Set<Domain.LoanApplication>();
    public DbSet<Operator> Operators => Set<Operator>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
