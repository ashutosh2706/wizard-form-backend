namespace WizardFormBackend.DTOs
{
    /* 
     * Not used
     **/
    public class PaginatedResponseDTO<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        IEnumerable<T>? Items { get; set; }
    }
}
