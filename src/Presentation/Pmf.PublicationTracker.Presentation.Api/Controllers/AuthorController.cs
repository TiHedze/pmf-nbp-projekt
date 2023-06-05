namespace Pmf.PublicationTracker.Presentation.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Pmf.PublicationTracker.Application.Commands.Author;
    using Pmf.PublicationTracker.Application.Contracts.DataTransferObjects;
    using Pmf.PublicationTracker.Application.Queries.Author;
    using Pmf.PublicationTracker.Domain.Entities;

    [ApiController]
    public sealed class AuthorController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthorController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [HttpGet]
        public async Task<List<Author>> Get([FromQuery] string? query = default)
            => await this.mediator.Send(new GetAuthors.Request(query));

        [HttpPost]
        public async Task<Guid> Post([FromBody] Author author)
            => await this.mediator.Send(new CreateAuthor.Command(author));

        [HttpPut]
        public async Task<Author> Put([FromBody] Author author)
           => await this.mediator.Send(new UpdateAuthor.Command(author));

        [HttpDelete("id")]
        public async Task Delete(Guid id)
           => await this.mediator.Send(new DeleteAuthor.Command(id));

        [HttpGet("{id}/details")]
        public async Task<AuthorDetails> Details(
                Guid id,
                CancellationToken cancellationToken)
                => await this.mediator.Send(new GetAuthorById.Request(id), cancellationToken);
    }
}
