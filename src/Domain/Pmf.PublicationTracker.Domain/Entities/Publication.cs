namespace Pmf.PublicationTracker.Domain.Entities
{
    using Pmf.PublicationTracker.Domain.Common;

    public class Publication: EntityBase
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Abstract { get; set; } = default!;
        public List<string> Keywords { get; set; } = default!;
        public List<Author> Authors { get; set; } = default!;
    }
}
