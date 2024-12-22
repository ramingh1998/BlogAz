using BlogAz.Domain.Interfaces.Blogs;
using BlogAz.Domain.Interfaces.Categories;
using BlogAz.Domain.Interfaces.Users;
using BlogAz.Infrastructure.Persistence;
using BlogAz.Infrastructure.Repositories.Blogs;
using BlogAz.Infrastructure.Repositories.Categories;
using BlogAz.Infrastructure.Repositories.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogAz.Infrastructure
{
    public static class InfrastructureBootstrapper
    {
        public static IServiceCollection InitInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IBlogCategoryRepository, BlogCategoryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddMediatR(typeof(InfrastructureBootstrapper).Assembly);
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("AppDbContext"));
            });
            return services;
        }
    }
}
