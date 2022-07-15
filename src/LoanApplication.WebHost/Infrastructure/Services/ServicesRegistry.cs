using LoanApplication.WebHost.Domain;

namespace LoanApplication.WebHost.Infrastructure.Services;

public static class ServicesRegistry
{
    public static WebApplicationBuilder AddCustomServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ICurrentDateProvider, SystemDateProvider>();
        builder.Services.AddTransient<ICurrentUserProvider, AdminUserProvider>();
        builder.Services.AddScoped<ILoanApplications, EntityFrameworkLoanApplications>();
        builder.Services.AddScoped<IOperators, EntityFrameworkOperators>();
        builder.Services.AddScoped<IDebtorRegistry, HardCodedDebtorRegistry>();

        return builder;
    }
}
