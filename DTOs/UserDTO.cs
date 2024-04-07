namespace WizardFormBackend.DTOs
{
    public class UserDTO
    {
        public long UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;     
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; } = 1;   // 1=user 2=admin
    }
}
