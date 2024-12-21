using BlogAz.AdminPresentationLayer.Infrastructure.RazorUtils;

namespace BlogAz.AdminPresentationLayer.Infrastructure;

public static class RegisterDependencyServices
{
    public static IServiceCollection RegisterWebDependencies(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddTransient<IRenderViewToString, RenderViewToString>();
        return services;
    }
}