namespace Pmf.PublicationTracker.Application.Contracts.Repositories
{
    using Pmf.PublicationTracker.Domain.Entities;

    public interface IPostgresRepository
    {
        public Task CreateAuthorAsync(Author author, CancellationToken cancellationToken);
        public Task DeleteAuthorAsync(Author author, CancellationToken cancellationToken);
        public Task<Author> UpdateAuthorAsync(Author author, CancellationToken cancellationToken);
        public Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<List<Author>> GetAuthorsAsync(CancellationToken cancellationToken);
    }
}
