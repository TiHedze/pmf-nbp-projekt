namespace Pmf.PublicationTracker.Presentation.Api.Internal.Requests
{
    using System.Collections.Generic;

    public sealed class PublicationRequest
    {
        public string Title { get; set; } = default!;
        public string Abstract { get; set; } = default!;
        public List<string> AuthorNames { get; set; } = default!;
        public string Keywords { get; set; } = default!;
    }
}
