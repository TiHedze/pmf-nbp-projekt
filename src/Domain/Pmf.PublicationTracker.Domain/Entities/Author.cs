namespace Pmf.PublicationTracker.Domain.Entities
{
    using Pmf.PublicationTracker.Domain.Common;

    public class Author : EntityBase
    {
        public Author() { }

        public Author(
            Guid id,
            string firstName,
            string lastName)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
