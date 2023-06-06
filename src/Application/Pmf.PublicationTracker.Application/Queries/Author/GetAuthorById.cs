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
        public record Request(Guid AuthorId): IRequest<AuthorDetails?>;
        internal sealed class Handler : IRequestHandler<Request, AuthorDetails?>
        {
            private readonly IPostgresRepository postgresRepository;
            private readonly INeo4jRepository neo4JRepository;

            public Handler(IPostgresRepository postgresRepository, INeo4jRepository neo4JRepository)
            {
                this.postgresRepository = postgresRepository;
                this.neo4JRepository = neo4JRepository;
            }

            public async Task<AuthorDetails?> Handle(
                Request request,
                CancellationToken cancellationToken)
            { 
                var author = await this.postgresRepository.GetAuthorByIdAsync(request.AuthorId, cancellationToken);

                if (author is null)
                {
                    return null;
                }

                var publications = await this.postgresRepository.GetPublicationsByAuthorAsync(author, cancellationToken);
                var relatedAuthorIds = await this.neo4JRepository.GetRelatedAuthorIds(author.Id);
                var relatedAuthors = await this.postgresRepository.GetAuthorsByIdAsync(relatedAuthorIds, cancellationToken);

                return new AuthorDetails(author, publications, relatedAuthors);
            }
        }
    }
}
