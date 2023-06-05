namespace Pmf.PublicationTracker.Application.Commands.Author
{
    using Domain.Entities;
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    public static class CreateAuthor
    {
        public record Command(string FirstName, string LastName) : IRequest;

        internal sealed class Handler : IRequestHandler<Command>
        {
            private readonly IPostgresRepository repository;
            private readonly INeo4jRepository neo4JRepository;

            public Handler(IPostgresRepository repository, INeo4jRepository neo4JRepository)
            {
                this.repository = repository;
                this.neo4JRepository = neo4JRepository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                Author author = new (Guid.NewGuid(), request.FirstName, request.LastName);
                await this.repository.CreateAuthorAsync(author, cancellationToken);
                await this.neo4JRepository.CreateAuthorAsync(author.Id);
            }
        }
    }
}
