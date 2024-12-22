
namespace BlogAz.Web.Infrastructure;

public static class RegisterDependencyServices
{
    public static IServiceCollection RegisterWebDependencies(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        return services;
    }
}