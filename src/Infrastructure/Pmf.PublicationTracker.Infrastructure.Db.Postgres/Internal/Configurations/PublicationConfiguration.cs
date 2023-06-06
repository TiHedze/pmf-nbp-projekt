namespace Pmf.PublicationTracker.Infrastructure.Db.Postgres.Internal.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Pmf.PublicationTracker.Domain.Entities;

    internal sealed class PublicationConfiguration : IEntityTypeConfiguration<Publication>
    {
        public void Configure(EntityTypeBuilder<Publication> entity)
        {
            entity.ToTable("publications", "public");

            entity.Property(e => e.Id)
                .HasColumnName("publication_id");

            entity.Property(e => e.Title)
                .HasColumnName("title");

            entity.Property(e => e.Abstract)
                .HasColumnName("abstract");

            entity.Property(e => e.Keywords)
                .HasColumnName("keywords");

            entity.Ignore(e => e.Authors);
        }
    }
}
