﻿namespace Pmf.PublicationTracker.Application
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddMediatR(config => 
                config.RegisterServicesFromAssembly(typeof(ApplicationModule).Assembly));

            return services;
        }

    }
}