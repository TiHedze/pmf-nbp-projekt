namespace Pmf.PublicationTracker.Presentation.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Pmf.PublicationTracker.Application.Queries.Author;

    [ApiController]
    public sealed class AuthorController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthorController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("id")]
        public async Task<AuthorDetails> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        => await this.mediator.Send(new GetAuthorById.Request(id), cancellationToken);

        [HttpGet]
        public async Task<List<AuthorDetails>> GetAuthorsByQuery([FromQuery] string? query = default)
            => await this.mediator.Send(new GetAuthorsByQuery)
    }
}
