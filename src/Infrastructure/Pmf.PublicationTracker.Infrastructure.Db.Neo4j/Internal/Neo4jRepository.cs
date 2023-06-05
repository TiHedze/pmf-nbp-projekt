namespace Pmf.PublicationTracker.Infrastructure.Db.Neo4j.Internal
{
    using Neo4jClient;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
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

        public Task AddAuthorsToPublication(Guid publicationId, List<Guid> authorIds) => throw new NotImplementedException();
        public async Task CreateAuthorAsync(Guid authorId)
        {
            await this.client.Cypher.Create($"(:Author {{id: {authorId}}})").ExecuteWithoutResultsAsync();
        }
        public async Task CreatePublicationAsync(Guid publicationId, List<Guid> auhtorIds, List<string> Keywords)
        {
            using (var transaction = this.client.Tx.BeginTransaction())
            {
                await this.client.Cypher.Create($"(:Publication {{id: {publicationId}}})").ExecuteWithoutResultsAsync();

                foreach (var authorId in auhtorIds)
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
        public Task<List<Guid>> GetRelatedAuthorIds(Guid authorId) => throw new NotImplementedException();
        public async Task RemoveAuthorAsync(Guid authorId)
        {
            await this.client.Cypher
                .Match($"(author: Author {{id: {authorId}}})")
                .DetachDelete("author")
                .ExecuteWithoutResultsAsync();
        }
        public Task RemoveAuthorsFromPublication(Guid publicationId, List<Guid> authorIds) => throw new NotImplementedException();
    }
}
