namespace Pmf.PublicationTracker.Infrastructure.Db.Postgres.Internal
{
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class PostgresRepository : IPostgresRepository
    {
        private readonly PostgresContext dbContext;

        public PostgresRepository(PostgresContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAuthorAsync(Author author, CancellationToken cancellationToken)
        {
            await this.dbContext.Authors.AddAsync(author, cancellationToken);
        }

        public async Task DeleteAuthorAsync(Author author, CancellationToken cancellationToken)
        {
            this.dbContext.Authors.Remove(author);
            await this.dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await this.dbContext.Authors.FindAsync(id, cancellationToken);
        }

        public Task<List<Author>> GetAuthorsAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new List<Author>()
            {
                new (Guid.NewGuid(), "Tilen1", "Tilen1"),
                new (Guid.NewGuid(), "Tilen2", "Tilen2")
            });
        }

        public async Task<Author> UpdateAuthorAsync(Author author, CancellationToken cancellationToken)
        {
            this.dbContext.Authors.Update(author);
            await this.dbContext.SaveChangesAsync(cancellationToken);
            return author;
        }
    }
}
