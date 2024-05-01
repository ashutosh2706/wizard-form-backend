namespace WizardFormBackend.DTOs.Paginated
{
    public class PaginatedUserResponseDTO
    {
        public int Page { get; set; }
        public int Total { get; set; }
        public int Limit { get; set; }
        public List<UserResponseDTO>? Users { get; set; }
    }
}
