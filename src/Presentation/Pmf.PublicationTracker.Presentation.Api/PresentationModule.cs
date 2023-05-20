namespace Pmf.PublicationTracker.Presentation.Api
{
    using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;

    public static class PresentationModule
    {
        public static IServiceCollection AddPresentationModule(this IServiceCollection services)
        {
            var assembly = typeof(PresentationModule).Assembly;
            services.AddControllersWithViews()
                .AddApplicationPart(assembly)
                .AddRazorRuntimeCompilation();

            services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
            {
                options.FileProviders.Add(new EmbeddedFileProvider(assembly));
            });

            return services;
        }
    }
}