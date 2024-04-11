namespace WizardFormBackend.DTOs
{
    public class RequestDTO
    {
        public long RequestId { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string GuardianName { get; set; } = string.Empty;
        public required DateOnly RequestDate { get; set; }
        public int PriorityCode { get; set; }
        public int StatusCode { get; set; }
        public string Phone { get; set; } = string.Empty;
        public IFormFile? AttachedFile { get; set; }
    }
}
