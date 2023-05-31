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
        public record Request(Author Author) : IRequest<List<Author>>;
        internal sealed class Handler : IRequestHandler<Request, List<Author>>
        {
            private readonly INeo4jRepository neo4JRepository;

            public Handler(INeo4jRepository neo4JRepository)
            {
                this.neo4JRepository = neo4JRepository;
            }

            public async Task<List<Author>> Handle(
                Request request,
                CancellationToken cancellationToken)
                => await this.neo4JRepository.GetRelatedAuthors(request.Author);
        }
    }
}
