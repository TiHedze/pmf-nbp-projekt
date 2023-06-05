namespace Pmf.PublicationTracker.Application.Contracts.DataTransferObjects
{
    public record AuthorDto(string FirstName, string LastName);

    public record AuthorDetails(object author, object publications, object relatedAuthors);
}
