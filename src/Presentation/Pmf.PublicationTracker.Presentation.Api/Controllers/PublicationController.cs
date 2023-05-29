namespace Pmf.PublicationTracker.Presentation.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Pmf.PublicationTracker.Presentation.Api.Internal.ViewModels;

    public sealed class PublicationController : Controller
    {
        private readonly IMediator mediator;

        public PublicationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult Index()
        {
            PublicationViewModel? viewModel = null; // fetching logic here
            return View(viewModel);
        }

        public async Task<IActionResult> Author(Guid authorId)
        {
            object? viewModel = null; //this.mediator.S
            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            throw new NotImplementedException();
        }

        //public async Task<IActionResult> 
    }
}
