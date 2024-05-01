namespace WizardFormBackend.DTOs
{
    public class PaginatedResponseDTO<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<T>? Items { get; set; }
    }
}
