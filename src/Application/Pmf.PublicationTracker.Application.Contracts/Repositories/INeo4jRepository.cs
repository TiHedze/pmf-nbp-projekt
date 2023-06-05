namespace Pmf.PublicationTracker.Application.Contracts.Repositories
{
    public interface INeo4jRepository
    {
        public Task CreateAuthorAsync(Guid authorId);
        public Task RemoveAuthorAsync(Guid authorId);
        public Task<List<Guid>> GetRelatedAuthorIds(Guid authorId);
        public Task CreatePublicationAsync(Guid publicationId, List<Guid> authorIds, List<string> Keywords);
        public Task AddAuthorsToPublication(Guid publicationId, List<Guid> authorIds);
        public Task RemoveAuthorsFromPublication(Guid publicationId, List<Guid> authorIds);
        public Task RemoveAllAuthorsFromPublicationAsync(Guid publicationId);
        public Task RemovePublicationAsync(Guid publicationId);
    }
}
