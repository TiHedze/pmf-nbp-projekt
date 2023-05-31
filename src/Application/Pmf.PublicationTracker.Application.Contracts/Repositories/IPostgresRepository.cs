namespace Pmf.PublicationTracker.Application.Contracts.Repositories
{
    using Pmf.PublicationTracker.Domain.Entities;

    public interface IPostgresRepository
    {
        public Task CreateAuthorAsync(Author author, CancellationToken cancellationToken);
        public Task DeleteAuthorAsync(Author author, CancellationToken cancellationToken);
        public Task<Author> UpdateAuthorAsync(Author author, CancellationToken cancellationToken);
        public Task<Author> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<List<Author>> GetAuthorsAsync(string? filter, CancellationToken cancellationToken);
        public Task<List<Author>> GetAuthorsByName(List<string> authorNames, CancellationToken cancellationToken);

        public Task CreatePublicationAsync(Publication publication, CancellationToken cancellationToken);
        public Task DeletePublicationAsync(Publication publication, CancellationToken cancellationToken);
        public Task<Publication> GetPublicationByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<Publication> UpdatePublicationAsync(Publication publication, CancellationToken cancellationToken);
        public Task<List<Publication>> GetPublicationsByTitleAsync(string title, CancellationToken cancellationToken);
        public Task<List<Publication>> GetPublicationsAsync(CancellationToken cancellationToken);
        public Task<List<Publication>> GetPublicationsByAuthorAsync(Author author, CancellationToken cancellationToken);
    }
}
