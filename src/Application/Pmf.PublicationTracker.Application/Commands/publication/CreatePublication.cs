namespace Pmf.PublicationTracker.Application.Commands.Publication
{
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.DataTransferObjects;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Presentation.Api.Internal.Requests;
    using System.Threading;
    using System.Threading.Tasks;

    public static class CreatePublication
    {
        public record Command(PublicationRequest Publication) : IRequest<Guid>;
        internal class Handler : IRequestHandler<Command, Guid>
        {
            private readonly IPostgresRepository postgresRepository;
            private readonly INeo4jRepository neo4jRepository;

            public Handler(IPostgresRepository postgresRepository, INeo4jRepository neo4jRepository)
            {
                this.postgresRepository = postgresRepository;
                this.neo4jRepository = neo4jRepository;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                var splittedAuthors = request.Publication.Authors
                        .Select(author => $"{author.FirstName}{author.LastName}")
                        .ToList();

                var splittedKeywords = request.Publication.Keywords.Split(',').ToList();

                var authors = await this.postgresRepository.GetAuthorsByName(splittedAuthors, cancellationToken);

                PublicationDto dto = new PublicationDto(Guid.NewGuid(), request.Publication.Title, request.Publication.Abstract,splittedKeywords, splittedAuthors);

                await this.postgresRepository.CreatePublicationAsync(dto, cancellationToken);
                await this.neo4jRepository.CreatePublicationAsync(dto.Id, authors.Select(author => author.Id).ToList(), splittedKeywords);

                return dto.Id;
            }
        }
    }
}
