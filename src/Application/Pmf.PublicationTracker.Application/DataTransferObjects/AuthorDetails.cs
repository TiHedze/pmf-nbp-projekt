namespace Pmf.PublicationTracker.Application.DataTransferObjects
{
    using Pmf.PublicationTracker.Domain.Entities;
    using System.Collections.Generic;

    public record AuthorDetails(Author Author, List<Publication> Publications, List<Author> RelatedAuthors);
}
