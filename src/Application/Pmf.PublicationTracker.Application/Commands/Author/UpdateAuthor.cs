namespace Pmf.PublicationTracker.Application.Commands.Author
{
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Domain.Entities;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public static class UpdateAuthor
    {
        public record Command(Author Author): IRequest;
        internal sealed class Handler : IRequestHandler<Command>
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
                await this.postgresRepository.UpdateAuthorAsync(request.Author, cancellationToken);
            }
        }
    }
}
