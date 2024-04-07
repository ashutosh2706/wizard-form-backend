using WizardFormBackend.DTOs;
using WizardFormBackend.Models;
using WizardFormBackend.Repositories;
using WizardFormBackend.Utils;

namespace WizardFormBackend.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

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
                    Active = user.Active ? "Allowed" : "Restricted"
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

        public async Task AllowUser(long userId, bool allowed)
        {
            User? user = await _userRepository.GetUserByUserIdAsync(userId);
            if (user != null)
            {
                user.Active = allowed;
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

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }
    }
}
