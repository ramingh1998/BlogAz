using BlogAz.Facade.Blogs;
using BlogAz.Facade.Categories;
using Microsoft.Extensions.DependencyInjection;

namespace BlogAz.Facade
{
    public static class FacadeBootstrapper
    {
        public static IServiceCollection InitFacadeDependencies(this IServiceCollection services)
        {
            services.AddScoped<IBlogFacade, BlogFacade>();
            services.AddScoped<ICategoryFacade, CategoryFacade>();
            return services;
        }
    }
}
