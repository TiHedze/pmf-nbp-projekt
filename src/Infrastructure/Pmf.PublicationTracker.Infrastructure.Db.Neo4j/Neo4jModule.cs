namespace Pmf.PublicationTracker.Infrastructure.Db.Neo4j
{
    using Microsoft.Extensions.DependencyInjection;
    using Neo4jClient;
    using Pmf.PublicationTracker.Infrastructure.Db.Neo4j.Internal.Settings;

    public static class Neo4jModule
    {
        public static IServiceCollection AddNeo4jModule(this IServiceCollection services, Neo4jSettings settings) 
        {
            services.AddSingleton<IGraphClient>(_ =>
            {
                var client = new BoltGraphClient(
                    settings.ConnectionString,
                    username: settings.Username,
                    password: settings.Password);
                
                client.ConnectAsync().Wait();
                
                return client;
            });

            return services;
        }
    }
}