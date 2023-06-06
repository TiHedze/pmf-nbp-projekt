namespace Pmf.PublicationTracker.Application.Queries.Author
{
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Domain.Entities;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public static class GetRelatedAuthors
    {
        public record Request(Guid AuthorId) : IRequest<List<Author>>;
        internal sealed class Handler : IRequestHandler<Request, List<Author>>
        {
            private readonly INeo4jRepository neo4JRepository;
            private readonly IPostgresRepository postgresRepository;

            public Handler(
                INeo4jRepository neo4JRepository,
                IPostgresRepository postgresRepository)
            {
                this.neo4JRepository = neo4JRepository;
                this.postgresRepository = postgresRepository;
            }

            public async Task<List<Author>> Handle(
                Request request,
                CancellationToken cancellationToken)
            {
                var relatedIds = await this.neo4JRepository.GetRelatedAuthorIds(request.AuthorId);

                var relatedAuthors = await this.postgresRepository.GetAuthorsByIdAsync(relatedIds, cancellationToken);

                return relatedAuthors;
            }
        }
    }
}
