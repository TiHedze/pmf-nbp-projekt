namespace Pmf.PublicationTracker.Domain.Common.Requests
{
    using Pmf.PublicationTracker.Presentation.Api.Internal.Requests;
    using System;

    public class UpdatePublicationRequest : PublicationRequest
    {
        public Guid Id { get; set; }
    }
}
