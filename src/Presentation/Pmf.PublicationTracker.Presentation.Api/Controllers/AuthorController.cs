namespace Pmf.PublicationTracker.Presentation.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Pmf.PublicationTracker.Application.Commands.Author;
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

        public async Task<IActionResult> Index(CancellationToken cancellationToken, string? filter = default)
        {
            var viewData = ViewModelMapper
                .Map<Author, AuthorViewModel>(await this.mediator.Send(new GetAuthors.Request(), cancellationToken));

            return View(viewData);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveAuthor(AuthorRequest author, CancellationToken cancellationToken)
        {
            await this.mediator.Send(new CreateAuthor.Command(author.FirstName, author.LastName), cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid authorId)
        {
            return View();
        }

        public async Task<IActionResult> Search(string query)
        {

            throw new NotImplementedException();
        }
    }
}
