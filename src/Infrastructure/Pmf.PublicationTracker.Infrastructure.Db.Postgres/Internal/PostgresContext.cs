namespace Pmf.PublicationTracker.Infrastructure.Db.Postgres.Internal
{
    using Microsoft.EntityFrameworkCore;
    using Pmf.PublicationTracker.Domain.Entities;

    internal sealed class PostgresContext : DbContext
    {
        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
        {
        }

        internal DbSet<Author> Authors { get; set; } = default!;
        internal DbSet<Publication> Publications { get; set; } = default!;
        
        protected override void OnModelCreating(ModelBuilder builder) => builder.ApplyConfigurationsFromAssembly(typeof(PostgresModule).Assembly);
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => base.OnConfiguring(optionsBuilder);
    }
}
