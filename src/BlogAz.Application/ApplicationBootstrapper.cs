using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BlogAz.Application
{
    public static class ApplicationBootstrapper
    {
        public static void InitApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<ILocalFileService, LocalFileService>();
            services.AddMediatR(typeof(ApplicationBootstrapper).Assembly);
            services.AddValidatorsFromAssembly(typeof(ApplicationBootstrapper).Assembly);
        }
    }
}
