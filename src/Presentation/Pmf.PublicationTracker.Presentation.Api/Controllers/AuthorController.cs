namespace Pmf.PublicationTracker.Presentation.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Pmf.PublicationTracker.Application.Queries.Author;
    using Pmf.PublicationTracker.Domain.Entities;
    using Pmf.PublicationTracker.Presentation.Api.Internal.Mappings;
    using Pmf.PublicationTracker.Presentation.Api.Internal.Requests;
    using Pmf.PublicationTracker.Presentation.Api.Internal.ViewModels;

    public sealed class AuthorController : Controller
    {
        private readonly IMediator mediator;

        public AuthorController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var viewData = ViewModelMapper
                .Map<Author, AuthorViewModel>(await this.mediator.Send(new GetAuthors.Request()));

            return View(viewData);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveAuthor(AuthorRequest author) 
        {
            //create author;

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid authorId)
        {
            return View();
        }
    }
}
