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

            public Handler(IPostgresRepository repository)
            {
                this.repository = repository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                Author author = new (Guid.NewGuid(), request.FirstName, request.LastName);
                await this.repository.CreateAuthorAsync(author, cancellationToken);
            }
        }
    }
}
