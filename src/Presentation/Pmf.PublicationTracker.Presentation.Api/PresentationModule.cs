namespace Pmf.PublicationTracker.Presentation.Api
{
    using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;

    public static class PresentationModule
    {
        public static IServiceCollection AddPresentationModule(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}