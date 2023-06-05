namespace Pmf.PublicationTracker.Application.Commands.Author
{
    using Domain.Entities;
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    public static class CreateAuthor
    {
        public record Command(Author Author) : IRequest<Guid>;

        internal sealed class Handler : IRequestHandler<Command, Guid>
        {
            private readonly IPostgresRepository repository;
            private readonly INeo4jRepository neo4JRepository;

            public Handler(IPostgresRepository repository, INeo4jRepository neo4JRepository)
            {
                this.repository = repository;
                this.neo4JRepository = neo4JRepository;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                Author author = new (Guid.NewGuid(), request.Author.FirstName, request.Author.LastName);
                await this.repository.CreateAuthorAsync(author, cancellationToken);
                await this.neo4JRepository.CreateAuthorAsync(author.Id);

                return author.Id;
            }
        }
    }
}
