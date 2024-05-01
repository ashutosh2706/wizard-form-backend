namespace WizardFormBackend.DTOs.Paginated
{
    public class PaginatedRequestDTO
    {
        public int Page { get; set; }
        public int Total { get; set; }
        public int Limit { get; set; }
        public List<RequestDTO>? Requests { get; set; }
    }
}
