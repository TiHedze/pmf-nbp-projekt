namespace Pmf.PublicationTracker.Infrastructure.Db.Postgres.Internal
{
    using Microsoft.EntityFrameworkCore;
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
            await this.dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAuthorAsync(Author author, CancellationToken cancellationToken)
        {
            this.dbContext.Authors.Remove(author);
            await this.dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await this.dbContext
                .Authors
                .FirstOrDefaultAsync(
                    author => author.Id == id,
                    cancellationToken);
        }

        public async Task<List<Author>> GetAuthorsAsync(string? filter, CancellationToken cancellationToken)
        {
            if (filter is not null)
            {
                return await this.dbContext
                    .Authors
                    .Where(author => (author.FirstName + author.LastName).Contains(filter))
                    .ToListAsync(cancellationToken);
            }

            return await this.dbContext
                .Authors
                .ToListAsync(cancellationToken);
        }

        public async Task<Author> UpdateAuthorAsync(Author author, CancellationToken cancellationToken)
        {
            this.dbContext.Authors.Update(author);
            await this.dbContext.SaveChangesAsync(cancellationToken);
            return author;
        }
    }
}
