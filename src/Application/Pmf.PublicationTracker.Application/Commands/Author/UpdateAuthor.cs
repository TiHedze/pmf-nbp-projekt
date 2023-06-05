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
        public record Command(Author Author): IRequest<Author?>;
        internal sealed class Handler : IRequestHandler<Command, Author?>
        {
            private readonly IPostgresRepository postgresRepository;

            public Handler(IPostgresRepository postgresRepository)
            {
                this.postgresRepository = postgresRepository;
            }

            public async Task<Author?> Handle(Command request, CancellationToken cancellationToken)
            {
                await this.postgresRepository.UpdateAuthorAsync(request.Author, cancellationToken);

                return await this.postgresRepository.GetAuthorByIdAsync(request.Author.Id, cancellationToken);
            }
        }
    }
}
