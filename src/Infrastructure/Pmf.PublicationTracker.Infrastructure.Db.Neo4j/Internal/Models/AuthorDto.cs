namespace Pmf.PublicationTracker.Infrastructure.Db.Neo4j.Internal.Models
{
    using Pmf.PublicationTracker.Domain.Entities;

    internal sealed class AuthorDto
    {
        internal AuthorDto(Author author)
        {
            this.Id = author.Id;
            this.Ime = author.FirstName;
            this.Prezime = author.LastName;
        }

        internal Guid Id { get; set; }
        internal string Ime { get; set; } = default!;
        internal string Prezime { get; set; } = default!;
    }
}
