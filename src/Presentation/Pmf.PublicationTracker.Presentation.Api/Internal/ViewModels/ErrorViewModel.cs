namespace Pmf.PublicationTracker.Presentation.Api.Internal.ViewModels
{
    public class ErrorViewModel
    {
        public ErrorViewModel(string? requestId)
        {
            this.RequestId = requestId;
        }
        public string? RequestId { get; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}