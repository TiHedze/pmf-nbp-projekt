namespace Pmf.PublicationTracker.Infrastructure.Db.Postgres.Internal.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Pmf.PublicationTracker.Domain.Entities;

    internal sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> entity)
        {
            entity.ToTable("authors", "public");

            entity.Property(e => e.Id)
                .HasColumnName("author_id");

            entity.Property(e => e.FirstName)
                .HasColumnName("first_name");

            entity.Property(e => e.LastName)
                .HasColumnName("last_name");
        }
    }
}
