namespace WizardFormBackend.DTOs
{
    public class RequestDTO
    {
        public long RequestId { get; set; }
        public long UserId { get; set; }
        public required string Title { get; set; }
        public required string GuardianName { get; set; }
        public required DateOnly RequestDate { get; set; }
        public required string Priority { get; set; }
        public string Phone { get; set; } = string.Empty;
    }
}
