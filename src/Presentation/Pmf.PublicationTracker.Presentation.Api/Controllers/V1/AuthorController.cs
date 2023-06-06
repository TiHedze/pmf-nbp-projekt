namespace Pmf.PublicationTracker.Presentation.Api.Controllers.V1
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Pmf.PublicationTracker.Application.Commands.Author;
    using Pmf.PublicationTracker.Application.Queries.Author;
    using Pmf.PublicationTracker.Domain.Entities;

    [Route("/api/[controller]")]
    [ApiController]
    public sealed class AuthorController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthorController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? query = default)
        {
            var result = await mediator.Send(new GetAuthors.Request(query));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Author author)
        {
            var result = await mediator.Send(new CreateAuthor.Command(author));

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Author author)
        {
            var result = await mediator.Send(new UpdateAuthor.Command(author));

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await mediator.Send(new DeleteAuthor.Command(id));
            return Ok();
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> Details(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAuthorById.Request(id), cancellationToken);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
