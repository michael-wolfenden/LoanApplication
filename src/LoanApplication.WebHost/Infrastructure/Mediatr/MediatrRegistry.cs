using LoanApplication.WebHost.Infrastructure.Mediatr.Behaviours;
using MediatR;

namespace LoanApplication.WebHost.Infrastructure.Mediatr;

public static class MediatrRegistry
{
    public static WebApplicationBuilder AddCustomMediatr(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(typeof(Program));
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehaviour<,>));

        return builder;
    }
}
