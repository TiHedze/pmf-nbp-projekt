namespace Pmf.PublicationTracker.Application.Commands.Publication
{
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.DataTransferObjects;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Domain.Common.Requests;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public static class UpdatePublication
    {
        public record Command(UpdatePublicationRequest Publication) : IRequest<Guid>;
        internal sealed class Handler : IRequestHandler<Command, Guid>
        {
            private readonly IPostgresRepository postgresRepository;
            private readonly INeo4jRepository neo4JRepository;

            public Handler(IPostgresRepository postgresRepository, INeo4jRepository neo4JRepository)
            {
                this.postgresRepository = postgresRepository;
                this.neo4JRepository = neo4JRepository;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                PublicationDto dto = new PublicationDto(
                    request.Publication.Id,
                    request.Publication.Title,
                    request.Publication.Abstract,
                    request.Publication.Keywords.Split(',').ToList(),
                    request.Publication.Authors
                        .Select(author => $"{author.FirstName}{author.LastName}")
                        .ToList());
                var authors = await this.postgresRepository.GetAuthorsByNameAsync(dto.AuthorNames, cancellationToken);
                await this.postgresRepository.UpdatePublicationAsync(dto, cancellationToken);
                await this.neo4JRepository.RemoveAllAuthorsFromPublicationAsync(dto.Id);
                await this.neo4JRepository.AddAuthorsToPublication(dto.Id, authors?.Select(author => author.Id).ToList() ?? new());

                return dto.Id;
            }
        }
    }
}
