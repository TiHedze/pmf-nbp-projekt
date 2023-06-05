namespace Pmf.PublicationTracker.Application.Queries.Author
{
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Application.Contracts.DataTransferObjects;
    using Pmf.PublicationTracker.Domain.Entities;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public static class GetAuthorById
    {
        public record Request(Guid AuthorId): IRequest<AuthorDetails>;
        internal sealed class Handler : IRequestHandler<Request, AuthorDetails>
        {
            private readonly IPostgresRepository postgresRepository;
            private readonly INeo4jRepository neo4JRepository;

            public Handler(IPostgresRepository postgresRepository, INeo4jRepository neo4JRepository)
            {
                this.postgresRepository = postgresRepository;
                this.neo4JRepository = neo4JRepository;
            }

            public async Task<AuthorDetails> Handle(
                Request request,
                CancellationToken cancellationToken)
            { 
                var author = await this.postgresRepository.GetAuthorByIdAsync(request.AuthorId, cancellationToken);
                var publications = await this.postgresRepository.GetPublicationsByAuthorAsync(author, cancellationToken);
                var relatedAuthors = await this.neo4JRepository.GetRelatedAuthorIds(author.Id);

                return new AuthorDetails(author, publications, relatedAuthors);
            }
        }
    }
}
