namespace WizardFormBackend.Data.Dto
{
    public class UserDto
    {
        public long UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        public int RoleId { get; set; } = 1;
    }
}
