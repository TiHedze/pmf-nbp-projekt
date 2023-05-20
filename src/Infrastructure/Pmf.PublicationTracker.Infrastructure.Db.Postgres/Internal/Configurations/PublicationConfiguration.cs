namespace Pmf.PublicationTracker.Infrastructure.Db.Postgres.Internal.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Pmf.PublicationTracker.Domain.Entities;
    using System;

    internal sealed class PublicationConfiguration : IEntityTypeConfiguration<Publication>
    {
        public void Configure(EntityTypeBuilder<Publication> builder) => throw new NotImplementedException();
    }
}
