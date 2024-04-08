using WizardFormBackend.DTOs;
using WizardFormBackend.Models;
using WizardFormBackend.Repositories;
using WizardFormBackend.Utils;

namespace WizardFormBackend.Services
{
    public class UserService(IUserRepository userRepository, IRoleService roleService, IConfiguration configuration) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _configuration = configuration;
        private readonly IRoleService _roleService = roleService;

        public async Task<User> AddUserAsync(UserDTO userDTO)
        {
            User user = new()
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Password = Util.GenerateHash(userDTO.Password),
                RoleId = userDTO.RoleId,
                Active = false
            };

            return await _userRepository.AddUserAsync(user);
        }

        public async Task<IEnumerable<UserResponseDTO>> GetUsersAsync()
        {
            IEnumerable<User> users = await _userRepository.GetAllUserAsync();
            List<UserResponseDTO> result = new List<UserResponseDTO>();
            foreach (User user in users)
            {
                UserResponseDTO userDTO = new UserResponseDTO
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsAllowed = user.Active ? "Allowed" : "Restricted"
                };
                result.Add(userDTO);
            }
            return result;

        }

        public async Task UpdateUserAsync(UserDTO userDTO)
        {
            User? existingUser = await _userRepository.GetUserByUserIdAsync(userDTO.UserId);
            if (existingUser != null)
            {
                existingUser.FirstName = userDTO.FirstName;
                existingUser.LastName = userDTO.LastName;
                existingUser.Email = userDTO.Email;
                existingUser.Password = Util.GenerateHash(userDTO.Password);

                await _userRepository.UpdateUserAsync(existingUser);
            }
        }

        public async Task AllowUserAsync(long userId)
        {
            User? user = await _userRepository.GetUserByUserIdAsync(userId);
            if (user != null)
            {
                user.Active = !user.Active;
                await _userRepository.UpdateUserAsync(user);
            }
        }


        public async Task DeleteUserAsync(long userId)
        {
            User? user = await _userRepository.GetUserByUserIdAsync(userId);
            if (user != null)
            {
                await _userRepository.DeleteUserAsync(user);
            }
        }

        public async Task<string> AuthenticateUserAsync(LoginDTO loginDTO)
        {
            string password = Util.GenerateHash(loginDTO.Password);
            User? existingUser = await _userRepository.GetUserByEmailAsync(loginDTO.Email);
            if (existingUser != null && existingUser.Password == password && existingUser.Active)
            {
                string roleType = await _roleService.GetRoleTypeAsync(existingUser.RoleId);
                if (roleType != string.Empty)
                {
                    return new AuthProvider(_configuration).GetToken(existingUser, roleType);
                }
            }

            return string.Empty;
        }

        public async Task<string> GetRoleTypeAsync(string email)
        {
            User? existingUser = await _userRepository.GetUserByEmailAsync(email);
            if(existingUser != null)
            {
                return await _roleService.GetRoleTypeAsync(existingUser.RoleId);
            }

            return string.Empty;
        }

    }
}
