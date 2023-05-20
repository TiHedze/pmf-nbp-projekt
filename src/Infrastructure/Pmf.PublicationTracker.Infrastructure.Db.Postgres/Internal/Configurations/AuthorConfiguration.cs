namespace Pmf.PublicationTracker.Infrastructure.Db.Postgres.Internal.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Pmf.PublicationTracker.Domain.Entities;

    internal sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder) => throw new NotImplementedException();
    }
}
