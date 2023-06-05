﻿namespace Pmf.PublicationTracker.Application.Commands.Publication
{
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.DataTransferObjects;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Domain.Common.Requests;
    using Pmf.PublicationTracker.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public static class UpdatePublication
    {
        public record Command(UpdatePublicationRequest Publication): IRequest;
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
                PublicationDto dto = new PublicationDto(
                    request.Publication.Id,
                    request.Publication.Title,
                    request.Publication.Abstract,
                    request.Publication.Keywords.Split(',').ToList(),
                    request.Publication.Authors
                        .Select(author => $"{author.FirstName}{author.LastName}")
                        .ToList());
                var authors = await this.postgresRepository.GetAuthorsByName(dto.AuthorNames, cancellationToken);
                await this.postgresRepository.UpdatePublicationAsync(dto, cancellationToken);
                await this.neo4JRepository.RemoveAllAuthorsFromPublicationAsync(dto.Id);
                await this.neo4JRepository.AddAuthorsToPublication(dto.Id, authors.Select(author => author.Id).ToList());
            }
        }
    }
}
