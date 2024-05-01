using Azure.Core;
using WizardFormBackend.DTOs;
using WizardFormBackend.DTOs.Paginated;
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
        
        public async Task<PaginatedUserResponseDTO> GetUsersAsync(string query, int page, int limit)
        {
            IEnumerable<User> users = await _userRepository.GetAllUserAsync(query);

            int totalPage = (int)Math.Ceiling((decimal)users.Count() / limit);              
            IEnumerable<User> paginatedUsers = users.Skip((page - 1) * limit).Take(limit).ToList();

            List<UserResponseDTO> result = [];

            foreach (User user in paginatedUsers)
            {
                UserResponseDTO userDTO = new()
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsAllowed = user.Active ? "allowed" : "restricted",
                    RoleId = user.RoleId
                };
                result.Add(userDTO);
            }
            return new PaginatedUserResponseDTO { Page = page, Limit = limit, Total = totalPage, Users = result };

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

        public async Task<string?> AuthenticateUserAsync(LoginDTO loginDTO)
        {
            string password = Util.GenerateHash(loginDTO.Password);
            User? existingUser = await _userRepository.GetUserByEmailAsync(loginDTO.Email);
            if (existingUser != null && existingUser.Password == password)
            {
                if(existingUser.Active)
                {
                    string roleType = await _roleService.GetRoleTypeAsync(existingUser.RoleId);
                    return new AuthProvider(_configuration).GetToken(existingUser, roleType);
                }
                else
                {
                    return "";
                }
            }
            return null;
        }

        public async Task ChangeRoleAsync(long userId, int roleId)
        {
            User? user = await _userRepository.GetUserByUserIdAsync(userId);
            if(user != null)
            {
                user.RoleId = roleId;
                await _userRepository.UpdateUserAsync(user);
            }
        }

    }
}
