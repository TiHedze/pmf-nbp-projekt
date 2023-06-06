namespace Pmf.PublicationTracker.Application.Queries.Publication
{
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.DataTransferObjects;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    public static class GetPublicationById
    {
        public record Request(Guid Id) : IRequest<PublicationDetails?>;

        internal sealed class Handler : IRequestHandler<Request, PublicationDetails?>
        {
            private readonly IPostgresRepository postgresRepository;
            public Handler(IPostgresRepository postgresRepository)
            {
                this.postgresRepository = postgresRepository;
            }

            public async Task<PublicationDetails?> Handle(Request request, CancellationToken cancellationToken)
            {
                var publication = await this.postgresRepository.GetPublicationByIdAsync(request.Id, cancellationToken);

                if (publication is null)
                {
                    return null;
                }

                var authors = await this.postgresRepository.GetAuthorsByPublicationIdAsync(request.Id, cancellationToken);

                return new PublicationDetails(publication.Id, publication.Title, publication.Abstract, publication.Keywords, authors);
            }
        }
    }
}
