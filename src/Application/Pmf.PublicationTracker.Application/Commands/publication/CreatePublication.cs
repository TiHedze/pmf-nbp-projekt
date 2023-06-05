namespace Pmf.PublicationTracker.Application.Commands.Publication
{
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.DataTransferObjects;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Domain.Entities;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public static class CreatePublication
    {
        public record Command(string Title, string Abstract, string Keywords, List<string> AuthorNames) : IRequest;
        internal class Handler : IRequestHandler<Command>
        {
            private readonly IPostgresRepository postgresRepository;
            private readonly INeo4jRepository neo4jRepository;

            public Handler(IPostgresRepository postgresRepository, INeo4jRepository neo4jRepository)
            {
                this.postgresRepository = postgresRepository;
                this.neo4jRepository = neo4jRepository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var authors = await this.postgresRepository.GetAuthorsByName(request.AuthorNames, cancellationToken);

                var publication = new Publication()
                {
                    Title = request.Title,
                    Abstract = request.Abstract,
                    Authors = authors,
                    Id = Guid.NewGuid(),
                    Keywords = request.Keywords.Split(',').ToList()
                };

                PublicationDto dto = new PublicationDto(publication.Id, publication.Title, publication.Abstract, publication.Keywords, request.AuthorNames)

                await this.postgresRepository.CreatePublicationAsync(dto, cancellationToken);
                await this.neo4jRepository.CreatePublicationAsync(dto.Id, authors.Select(author => author.Id).ToList(), publication.Keywords);
            }
        }
    }
}
