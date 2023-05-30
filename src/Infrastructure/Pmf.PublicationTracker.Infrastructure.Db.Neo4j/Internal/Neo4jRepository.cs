namespace Pmf.PublicationTracker.Infrastructure.Db.Neo4j.Internal
{
    using Neo4jClient;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Domain.Entities;
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

        public async Task CreateAuthorAsync(Author author)
        {
            await this.client.Cypher
                 .Create(":Autor {newAuthor}")
                 .WithParam("newAuthor", new AuthorDto(author))
                 .ExecuteWithoutResultsAsync();
        }

        public async Task CreatePublicationAsync(Publication publication)
        {
            var batch = publication.Authors.Select(author => new {author, publication.Title }).ToList();    
            //this.client.Cypher.Unwind()
        }

        public async Task<List<Author>> GetRelatedAuthors(Author author)
        {
            IEnumerable<Author> coAuthors = await this.client.Cypher
                .Match("(firstAuthor:Author)-[:AUTHORED]->(:Paper)<-[:AUTHORED]-(coAuthor: Author)")
                .Where((Author firstAuthor, Author coAuthor) => author.Id != coAuthor.Id)
                .AndWhere((Author firstAuthor) => firstAuthor.Id != author.Id)
                .Return<Author>("coAuthor")
                .ResultsAsync;

            return coAuthors.ToList();
        }
        public async Task RemoveAuthorAsync(Author author) => throw new NotImplementedException();
    }
}
