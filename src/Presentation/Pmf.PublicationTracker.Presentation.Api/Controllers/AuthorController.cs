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

        public async Task<IActionResult> Index(string? filter = default)
        {
            //var viewData = ViewModelMapper
                //.Map<Author, AuthorViewModel>(await this.mediator.Send(new GetAuthors.Request(filter), cancellationToken));

            return View();
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

        public async Task<IActionResult> Details(Guid authorId, CancellationToken cancellationToken)
        {
            var (author, publications, relatedAuthors) = await this.mediator.Send(new GetAuthorById.Request(authorId), cancellationToken);
            var viewModel = new AuthorDetailsViewModel()
            {
                Author = ViewModelMapper.Map<Author, AuthorViewModel>(author),
                Publications = ViewModelMapper.Map<Publication, PublicationViewModel>(publications),
                RelatedAuthors = ViewModelMapper.Map<Author, AuthorViewModel>(relatedAuthors)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Search(string query)
        {

            return RedirectToAction(nameof(Index), new { query });
        }
    }
}
