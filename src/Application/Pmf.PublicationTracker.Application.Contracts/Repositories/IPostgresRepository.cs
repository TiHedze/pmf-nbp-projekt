﻿namespace Pmf.PublicationTracker.Application.Contracts.Repositories
{
    using Pmf.PublicationTracker.Application.Contracts.DataTransferObjects;
    using Pmf.PublicationTracker.Domain.Entities;

    public interface IPostgresRepository
    {
        public Task CreateAuthorAsync(Author author, CancellationToken cancellationToken);
        public Task DeleteAuthorAsync(Guid id, CancellationToken cancellationToken);
        public Task<Author> UpdateAuthorAsync(Author author, CancellationToken cancellationToken);
        public Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<List<Author>> GetAuthorsAsync(string filter, CancellationToken cancellationToken);
        public Task<List<Author>> GetAuthorsAsync(CancellationToken cancellationToken);
        public Task<List<Author>> GetAuthorsByNameAsync(List<string> authorNames, CancellationToken cancellationToken);
        public Task<List<Author>> GetAuthorsByIdAsync(List<Guid> authorIds, CancellationToken cancellationToken);
        public Task<List<Author>> GetAuthorsByPublicationIdAsync(Guid id, CancellationToken cancellationToken);

        public Task CreatePublicationAsync(PublicationDto publication, CancellationToken cancellationToken);
        public Task DeletePublicationAsync(Guid id, CancellationToken cancellationToken);
        public Task<Publication?> GetPublicationByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<Guid> UpdatePublicationAsync(PublicationDto publication, CancellationToken cancellationToken);
        public Task<List<Publication>> GetPublicationsByTitleAsync(string title, CancellationToken cancellationToken);
        public Task<List<Publication>> GetPublicationsAsync(CancellationToken cancellationToken);
        public Task<List<Publication>> GetPublicationsByAuthorAsync(Author author, CancellationToken cancellationToken);
    }
}
