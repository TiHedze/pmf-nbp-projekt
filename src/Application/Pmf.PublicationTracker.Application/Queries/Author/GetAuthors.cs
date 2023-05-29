namespace Pmf.PublicationTracker.Application.Queries.Author
{
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Domain.Entities;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public static class GetAuthors
    {
        public record Request(string QueryString) : IRequest<List<Author>>;

        internal class Handler : IRequestHandler<Request, List<Author>>
        {
            private readonly IPostgresRepository repository;

            public Handler(IPostgresRepository repository)
            {
                this.repository = repository;
            }

            public async Task<List<Author>> Handle(
                Request request, 
                CancellationToken cancellationToken)
                => await this.repository.GetAuthorsAsync(cancellationToken);
        }
    }
}
