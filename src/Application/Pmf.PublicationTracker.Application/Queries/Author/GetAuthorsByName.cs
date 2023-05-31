namespace Pmf.PublicationTracker.Application.Queries.Author
{
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Domain.Entities;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public static class GetAuthorsByName
    {
        public record Request(List<string> AuthorNames) : IRequest<List<Author>>;
        internal sealed class Handler : IRequestHandler<Request, List<Author>>
        {
            private readonly IPostgresRepository postgresRepository;

            public Handler(IPostgresRepository postgresRepository)
            {
                this.postgresRepository = postgresRepository;
            }

            public async Task<List<Author>> Handle(
                Request request,
                CancellationToken cancellationToken)
                => await this.postgresRepository.GetAuthorsByName(request.AuthorNames, cancellationToken);
        }
    }
}
