namespace Pmf.PublicationTracker.Presentation.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Pmf.PublicationTracker.Application.Commands.Publication;
    using Pmf.PublicationTracker.Domain.Common.Requests;
    using Pmf.PublicationTracker.Presentation.Api.Internal.Requests;

    [ApiController]
    public sealed class PublicationController : ControllerBase
    {
        private readonly IMediator mediator;

        public PublicationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<Guid> Post([FromBody] PublicationRequest publication)
            => await this.mediator.Send(new CreatePublication.Command(publication));

        [HttpPut]
        public async Task<Guid> Put([FromBody] UpdatePublicationRequest publication)
           => await this.mediator.Send(new UpdatePublication.Command(publication));

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
            => await this.mediator.Send(new DeletePublication.Command(id));
    }
}
