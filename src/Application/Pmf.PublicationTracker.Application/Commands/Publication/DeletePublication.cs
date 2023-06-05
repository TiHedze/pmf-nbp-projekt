﻿namespace Pmf.PublicationTracker.Application.Commands.Publication
{
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public static class DeletePublication
    {
        public record Command(Guid Id) : IRequest;

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
                var publication = await this.postgresRepository.GetPublicationByIdAsync(request.Id, cancellationToken);
                await this.postgresRepository.DeletePublicationAsync(publication!, cancellationToken);
                await this.neo4JRepository.RemovePublicationAsync(publication!.Id);
            }
        }
    }
}
