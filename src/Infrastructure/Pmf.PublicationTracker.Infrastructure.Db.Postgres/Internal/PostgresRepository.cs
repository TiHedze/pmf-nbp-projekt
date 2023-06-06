﻿namespace Pmf.PublicationTracker.Infrastructure.Db.Postgres.Internal
{
    using Microsoft.EntityFrameworkCore;
    using Npgsql;
    using Pmf.PublicationTracker.Application.Contracts.DataTransferObjects;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Domain.Entities;
    using Pmf.PublicationTracker.Infrastructure.Db.Postgres.Internal.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
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

        public async Task CreatePublicationAsync(PublicationDto publication, CancellationToken cancellationToken)
        {
            DbConnection connection = this.dbContext.Database.GetDbConnection();
            try
            {
                await using DbCommand command = connection.CreateCommand();

                command.CommandText = "CALL proc_publication_insert(@id, @title, @abstract, @keywords, @author_full_names)";

                command.Parameters.AddRange(
                   new[] {
                new NpgsqlParameter()
                {
                    ParameterName = "id",
                    Value = publication.Id,
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Uuid
                },
                new NpgsqlParameter()
                {
                    ParameterName = "title",
                    Value = publication.Title,
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar
                },
                new NpgsqlParameter()
                {
                    ParameterName = "abstract",
                    Value = publication.Abstract,
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar
                },
                new NpgsqlParameter()
                {
                    ParameterName = "keywords",
                    Value = string.Join(',',publication.Keywords),
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar
                },
                new NpgsqlParameter()
                {
                    ParameterName = "author_full_names",
                    Value = publication.AuthorNames.ToArray(),
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Varchar
                }
                   });

                await connection.OpenAsync(cancellationToken);
                await command.ExecuteNonQueryAsync(cancellationToken);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public async Task DeleteAuthorAsync(Guid id, CancellationToken cancellationToken)
        {
            await this.dbContext.Authors.Where(a => a.Id == id).ExecuteDeleteAsync(cancellationToken);
        }

        public async Task DeletePublicationAsync(Guid id, CancellationToken cancellationToken)
            => await this.dbContext.Publications.Where(p => p.Id == id).ExecuteDeleteAsync(cancellationToken);

        public async Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await this.dbContext
                .Authors
                .FirstOrDefaultAsync(
                    author => author.Id == id,
                    cancellationToken);
        }

        public async Task<List<Author>> GetAuthorsAsync(string filter, CancellationToken cancellationToken)
        {
            DbConnection connection = this.dbContext.Database.GetDbConnection();
            await using DbCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM public.fn_author_get(@full_name_substring)";

            command.Parameters.Add(
                new NpgsqlParameter()
                {
                    ParameterName = "full_name_substring",
                    Value = filter,
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar,
                });

            return await command.GetAsync<Author>(this.dbContext, cancellationToken);
        }

        public async Task<List<Author>> GetAuthorsAsync(CancellationToken cancellationToken)
            => await this.dbContext
                .Authors
                .ToListAsync(cancellationToken);

        public async Task<List<Author>> GetAuthorsByIdAsync(List<Guid> authorIds, CancellationToken cancellationToken) 
            => await this.dbContext.Authors
                .Where(author => authorIds.Contains(author.Id))
                .ToListAsync(cancellationToken);

        public async Task<List<Author>> GetAuthorsByNameAsync(List<string> authorNames, CancellationToken cancellationToken)
        {
            DbConnection connection = this.dbContext.Database.GetDbConnection();
            await using DbCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM public.fn_author_get_many(@author_full_names)";

            command.Parameters.Add(
                new NpgsqlParameter()
                {
                    ParameterName = "author_full_names",
                    Value = authorNames.ToArray(),
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Varchar,
                });

            return await command.GetAsync<Author>(this.dbContext, cancellationToken);
        }

        public async Task<List<Author>> GetAuthorsByPublicationIdAsync(Guid id, CancellationToken cancellationToken)
        {
            DbConnection connection = this.dbContext.Database.GetDbConnection();
            await using DbCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM public.fn_author_get_by_publication_id(@publication_id)";

            command.Parameters.Add(
                new NpgsqlParameter()
                {
                    ParameterName = "publication_id",
                    Value = id,
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Uuid
                });

            return await command.GetAsync<Author>(this.dbContext, cancellationToken);
        }
        public async Task<Publication?> GetPublicationByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
            => await this.dbContext.Publications.FirstOrDefaultAsync(publication => publication.Id == id, cancellationToken);
        public async Task<List<Publication>> GetPublicationsAsync(CancellationToken cancellationToken)
            => await this.dbContext.Publications.ToListAsync(cancellationToken);
        public async Task<List<Publication>> GetPublicationsByAuthorAsync(Author author, CancellationToken cancellationToken)
        {
            DbConnection connection = this.dbContext.Database.GetDbConnection();
            await using DbCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM public.fn_publication_get_by_author_id(@author_id)";

            command.Parameters.Add(
                new NpgsqlParameter()
                {
                    ParameterName = "author_id",
                    Value = author.Id,
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Uuid
                });

            return await command.GetAsync<Publication>(this.dbContext, cancellationToken);
        }
        public async Task<List<Publication>> GetPublicationsByTitleAsync(string title, CancellationToken cancellationToken)
            => await this.dbContext.Publications
                .Where(publication => publication.Title.Contains(title))
                .ToListAsync(cancellationToken);
        public async Task<Author> UpdateAuthorAsync(Author author, CancellationToken cancellationToken)
        {
            this.dbContext.Authors.Update(author);
            await this.dbContext.SaveChangesAsync(cancellationToken);
            return author;
        }

        public async Task<Guid> UpdatePublicationAsync(PublicationDto publication, CancellationToken cancellationToken)
        {
            DbConnection connection = this.dbContext.Database.GetDbConnection();
            try
            {
                await using DbCommand command = connection.CreateCommand();

                command.CommandText = "CALL proc_publication_update(@id, @title, @abstract, @keywords, @author_full_names)";

                command.Parameters.AddRange(new[]
                {
                new NpgsqlParameter()
                {
                    ParameterName = "id",
                    Value = publication.Id,
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Uuid
                },
                new NpgsqlParameter()
                {
                    ParameterName = "title",
                    Value = publication.Title,
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar
                },
                new NpgsqlParameter()
                {
                    ParameterName = "abstract",
                    Value = publication.Abstract,
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar
                },
                new NpgsqlParameter()
                {
                    ParameterName = "keywords",
                    Value = publication.Keywords,
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar
                },
                new NpgsqlParameter()
                {
                    ParameterName = "author_full_names",
                    Value = publication.AuthorNames.ToArray(),
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Varchar
                }
                });

                await connection.OpenAsync(cancellationToken);
                await command.ExecuteNonQueryAsync(cancellationToken);

                return publication.Id;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}
