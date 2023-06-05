namespace Pmf.PublicationTracker.Infrastructure.Db.Neo4j.Internal
{
    using Neo4jClient;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Infrastructure.Db.Neo4j.Internal.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal sealed class Neo4jRepository : INeo4jRepository
    {
        private readonly IGraphClient client;

        public Neo4jRepository(IGraphClient client)
        {
            this.client = client;
        }

        public async Task AddAuthorsToPublication(Guid publicationId, List<Guid> authorIds)
        {
            using(var transaction = this.client.Tx.BeginTransaction())
            {
                foreach (var authorId in authorIds)
                {
                    await this.client.Cypher
                        .Match($"(author: Author {{id: {authorId}}})")
                        .Match($"(publication: Publication {{id: {publicationId}}})")
                        .Merge($"(author)-[:WROTE]->(publication)")
                        .ExecuteWithoutResultsAsync();
                }
            }
        }
        public async Task CreateAuthorAsync(Guid authorId)
        {
            await this.client.Cypher
                .Create($"(:Author {{id: {authorId}}})")
                .ExecuteWithoutResultsAsync();
        }
        public async Task CreatePublicationAsync(Guid publicationId, List<Guid> authorIds, List<string> Keywords)
        {
            using (var transaction = this.client.Tx.BeginTransaction())
            {
                await this.client.Cypher.Create($"(:Publication {{id: {publicationId}}})").ExecuteWithoutResultsAsync();

                foreach (var authorId in authorIds)
                {
                    await this.client.Cypher
                        .Match($"(author: Author {{id: {authorId}}})")
                        .Match($"(publication: Publication {{id: {publicationId}}})")
                        .Merge($"(author)-[:WROTE]->(publication)")
                        .ExecuteWithoutResultsAsync();
                }

                foreach (var keyword in Keywords)
                {
                    await this.client.Cypher
                        .Match($"(keyword: Keyword {{value: {keyword}}})")
                        .Match($"(publication: Publication {{id: {publicationId}}})")
                        .Merge($"(keyword)-[:DESCRIBES]->(publication)")
                        .ExecuteWithoutResultsAsync();
                }
            }
        }
        public async Task<List<Guid>> GetRelatedAuthorIds(Guid authorId)
        {
            var authors = await this.client.Cypher
                .Match($"(author: Author {{id: {authorId}}})-[:WROTE]->(:Publication)<-[:WROTE]-(coauthor: Author)")
                .Where((Author author, Author coauthor) => author.Id != coauthor.Id)
                .Return(coauthor => coauthor.As<Author>())
                .ResultsAsync;

            return authors
                .Select(author => author.Id)
                .ToList();
                
        }

        public async Task RemoveAllAuthorsFromPublicationAsync(Guid publicationId)
        {
            await this.client.Cypher
                .Match($"(author: Author)-[:WROTE]->(publication: Publication {{id: {publicationId}}})")
                .DetachDelete("author")
                .ExecuteWithoutResultsAsync();
        }

        public async Task RemoveAuthorAsync(Guid authorId)
        {
            await this.client.Cypher
                .Match($"(author: Author {{id: {authorId}}})")
                .DetachDelete("author")
                .ExecuteWithoutResultsAsync();
        }
        public async Task RemoveAuthorsFromPublication(Guid publicationId, List<Guid> authorIds)
        {
            await this.client.Cypher
                .Match($"(author: Author)-[:WROTE]->(publication: Publication {{id: {publicationId}}})")
                .Where((Author author, Publication publication) => authorIds.Contains(author.Id))
                .DetachDelete("author")
                .ExecuteWithoutResultsAsync();
        }
    }
}
