namespace Pmf.PublicationTracker.Domain.Models
{
    using Pmf.PublicationTracker.Domain.Common.ViewModels;
    using Pmf.PublicationTracker.Domain.Entities;

    public class AuthorViewModel : ViewModelBase, IConstructibleFromDomainEntity<Author, AuthorViewModel>
    {
        private AuthorViewModel(Author author) 
        {
            this.FirstName = author.FirstName;
            this.LastName = author.LastName;
        }

        public string FirstName { get; } = default!;
        public string LastName { get; } = default!;

        public static AuthorViewModel FromEntity(Author entity) => new (entity);
    }
}
