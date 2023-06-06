namespace Pmf.PublicationTracker.Presentation.Api.Controllers.V1
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Pmf.PublicationTracker.Application.Commands.Publication;
    using Pmf.PublicationTracker.Application.Queries.Publication;
    using Pmf.PublicationTracker.Domain.Common.Requests;
    using Pmf.PublicationTracker.Presentation.Api.Internal.Requests;

    [Route("/api/[controller]")]
    [ApiController]
    public sealed class PublicationController : ControllerBase
    {
        private readonly IMediator mediator;

        public PublicationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PublicationRequest publication)
        {
            var result = await mediator.Send(new CreatePublication.Command(publication));

            return Ok(result);

        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdatePublicationRequest publication)
        {
            var result = await mediator.Send(new UpdatePublication.Command(publication));

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await mediator.Send(new DeletePublication.Command(id));
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? filter = default)
        {
            var result = await mediator.Send(new GetPublications.Request(filter));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await mediator.Send(new GetPublicationById.Request(id));

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
