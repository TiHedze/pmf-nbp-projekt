namespace Pmf.PublicationTracker.Application.Contracts.DataTransferObjects
{
    using System;
    using System.Collections.Generic;

    public sealed class PublicationDto
    {
        public PublicationDto(Guid id, string title, string @abstract, List<string> keywords, List<string> authorNames)
        {
            this.Id = id;
            this.Title = title;
            this.Abstract = @abstract;
            this.Keywords = keywords;
            this.AuthorNames = authorNames;
        }

        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Abstract { get; set; } = default!;
        public List<string> Keywords { get; set; } = default!;
        public List<string> AuthorNames { get; set; } = default!;
    }
}
