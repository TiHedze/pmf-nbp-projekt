namespace Pmf.PublicationTracker.Application.Queries.Author
{
    using MediatR;
    using Pmf.PublicationTracker.Application.Contracts.Repositories;
    using Pmf.PublicationTracker.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public static class GetAuthors
    {
        public record Request(): IRequest<List<AuthorViewModel>>;

        internal class Handler : IRequestHandler<Request, List<AuthorViewModel>>
        {
            private readonly IPostgresRepository repository;

            public Handler(IPostgresRepository repository)
            {
                this.repository = repository;
            }

            public async Task<List<AuthorViewModel>> Handle(Request request, CancellationToken cancellationToken)
            {
                var authors = await this.repository.GetAuthorsAsync(cancellationToken);
                return authors
                    .Select(AuthorViewModel.FromEntity)
                    .ToList();
            }
        }
    }
}
