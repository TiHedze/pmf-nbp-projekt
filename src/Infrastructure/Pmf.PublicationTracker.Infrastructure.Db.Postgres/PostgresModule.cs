namespace Pmf.PublicationTracker.Infrastructure.Db.Postgres
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Infrastructure.Db.Postgres.Internal;

    public static class PostgresModule
    {
        public static IServiceCollection AddPostgresModule(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PostgresContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IPostgresRepository, PostgresRepository>();

            return services;
        }
    }
}