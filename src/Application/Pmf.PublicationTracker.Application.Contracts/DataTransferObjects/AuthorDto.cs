namespace Pmf.PublicationTracker.Application.Contracts.DataTransferObjects
{
    using Pmf.PublicationTracker.Domain.Entities;

    public record AuthorDto(string FirstName, string LastName);

    public record AuthorDetails(Author Author, List<Publication> Publications, List<Author> RelatedAuthors);
}
