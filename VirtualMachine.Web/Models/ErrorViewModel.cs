namespace VirtualMachine.Web.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public string? ErrorMessage { get; set; }
        public string? StackTrace { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
