namespace Pmf.PublicationTracker.Presentation.Api.Internal.ViewModels
{
    using Pmf.PublicationTracker.Domain.Common.ViewModels;
    using System.Collections.Generic;

    public class AuthorDetailsViewModel : ViewModelBase
    {
        public AuthorViewModel Author { get; set; } = default!;
        public List<PublicationViewModel> Publications { get; set; } = default!;
        public List<AuthorViewModel> RelatedAuthors { get; set; } = default!;
    }
}
