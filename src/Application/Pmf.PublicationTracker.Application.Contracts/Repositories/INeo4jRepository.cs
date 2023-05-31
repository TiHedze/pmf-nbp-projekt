namespace Pmf.PublicationTracker.Application.Contracts.Repositories
{
    using Pmf.PublicationTracker.Domain.Entities;

    public interface INeo4jRepository
    {
        public Task CreateAuthorAsync(Author author);
        public Task RemoveAuthorAsync(Author author);
        public Task UpdateAuthorAsync(Author author);
        public Task<List<Author>> GetRelatedAuthors(Author author);
        public Task CreatePublicationAsync(Publication publication);
        public Task UpdatePublicationAsync(Publication publication);
    }
}
