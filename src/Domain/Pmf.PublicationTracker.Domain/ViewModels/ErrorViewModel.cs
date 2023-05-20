namespace Pmf.PublicationTracker.Models
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