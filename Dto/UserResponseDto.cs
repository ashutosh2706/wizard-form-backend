namespace WizardFormBackend.Dto
{
    public class UserResponseDto
    {
        public long UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string IsAllowed { get; set; }
        public required int RoleId { get; set; }
    }
}
