namespace LoanApplication.WebHost.Infrastructure.Web;

public static class WebRegistry
{
    public static WebApplicationBuilder AddCustomWeb(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        return builder;
    }
}
