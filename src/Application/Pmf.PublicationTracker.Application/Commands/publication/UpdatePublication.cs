namespace Pmf.PublicationTracker.Application.Commands.Publication
{
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public static class UpdatePublication
    {
        public record Command(Publication publication): IRequest;
        internal sealed class Handler : IRequestHandler<Command>
        {
            private readonly IPostgresRepository postgresRepository;
            private readonly INeo4jRepository neo4JRepository;

            public Handler(IPostgresRepository postgresRepository, INeo4jRepository neo4JRepository)
            {
                this.postgresRepository = postgresRepository;
                this.neo4JRepository = neo4JRepository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await this.postgresRepository.UpdatePublicationAsync(request.publication, cancellationToken);
                await this.neo4JRepository.UpdatePublicationAsync(request.publication);
            }
        }
    }
}
