namespace Pmf.PublicationTracker.Presentation.Api.Internal.ViewModels
{
    using Pmf.PublicationTracker.Domain.Common.ViewModels;
    using Pmf.PublicationTracker.Domain.Entities;
    using Pmf.PublicationTracker.Presentation.Api.Internal.Mappings;
    using System.Collections.Generic;

    public class PublicationViewModel : ViewModelBase, IConstructibleFromDomainEntity<Publication, PublicationViewModel>
    {
        private PublicationViewModel(Publication publication)
        {
            this.Title = publication.Title;
            this.Abstract = publication.Abstract;
            this.Keywords = publication.Keywords;
            this.Authors = ViewModelMapper.Map<Author, AuthorViewModel>(publication.Authors);
        }

        public string Title { get; set; } = default!;
        public string Abstract { get; set; } = default!;
        public List<string> Keywords { get; set; } = default!;
        public List<AuthorViewModel> Authors { get; set; } = default!;

        public static PublicationViewModel FromEntity(Publication entity) => new(entity);
    }
}
