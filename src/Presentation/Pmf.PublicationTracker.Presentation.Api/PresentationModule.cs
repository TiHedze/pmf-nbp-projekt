namespace Pmf.PublicationTracker.Presentation.Api
{
    using Microsoft.Extensions.DependencyInjection;

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