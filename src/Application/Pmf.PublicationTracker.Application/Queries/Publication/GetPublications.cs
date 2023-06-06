namespace Pmf.PublicationTracker.Application.Queries.Publication
{
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Domain.Entities;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public static class GetPublications
    {
        public record Request(string? Filter) : IRequest<List<Publication>>;

        internal sealed class Handler : IRequestHandler<Request, List<Publication>>
        {
            private readonly IPostgresRepository postgresRepository;

            public Handler(IPostgresRepository postgresRepository)
            {
                this.postgresRepository = postgresRepository;
            }

            public async Task<List<Publication>> Handle(Request request, CancellationToken cancellationToken)
                => request.Filter is not null ?
                await this.postgresRepository.GetPublicationsByTitleAsync(request.Filter, cancellationToken) :
                await this.postgresRepository.GetPublicationsAsync(cancellationToken);
        }
    }
}
