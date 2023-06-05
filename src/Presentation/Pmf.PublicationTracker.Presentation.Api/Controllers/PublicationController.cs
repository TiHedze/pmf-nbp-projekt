namespace Pmf.PublicationTracker.Presentation.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Pmf.PublicationTracker.Presentation.Api.Internal.Requests;

    [ApiController]
    public sealed class PublicationController : ControllerBase
    {
        private readonly IMediator mediator;

        public PublicationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult Index(string? query = default)
        {
            return View(viewModel);
        }

        public async Task<IActionResult> Author(Guid authorId)
        {
            object? viewModel = null; //this.mediator.S
            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SavePublication(PublicationRequest publication, CancellationToken cancellationToken)
        {
            

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
