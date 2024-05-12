using AutoMapper;
using WizardFormBackend.Data.Dto;
using WizardFormBackend.Data.Models;
using WizardFormBackend.Data.Repositories;
using WizardFormBackend.Utils;

namespace WizardFormBackend.Services
{
    public class UserService(IUserRepository userRepository, IRoleService roleService, IConfiguration configuration, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _configuration = configuration;
        private readonly IRoleService _roleService = roleService;
        private readonly IMapper _mapper = mapper;

        public async Task<User> AddUserAsync(UserDto userDto)
        {
            User user = _mapper.Map<UserDto, User>(userDto);
            user.Password = Util.GenerateHash(userDto.Password);
            user.Active = false;

            return await _userRepository.AddUserAsync(user);
        }
        
        public async Task<PagedResponseDto<UserResponseDto>> GetUsersAsync(string searchTerm, int pageNumber, int pageSize)
        {
            IEnumerable<User> users = await _userRepository.GetAllUserAsync(searchTerm);

            int totalPage = (int)Math.Ceiling((decimal)users.Count() / pageSize);              
            IEnumerable<User> paginatedUsers = users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            List<UserResponseDto> result = [];

            foreach (User user in paginatedUsers)
            {
                UserResponseDto userDTO = new()
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
            return new PagedResponseDto<UserResponseDto> { PageNumber = pageNumber, TotalPage = totalPage, PageSize = pageSize, Items = result };

        }

        public async Task UpdateUserAsync(UserDto userDTO)
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

        public async Task<bool> AllowUserAsync(long userId)
        {
            User? user = await _userRepository.GetUserByUserIdAsync(userId);
            if (user != null)
            {
                user.Active = !user.Active;
                await _userRepository.UpdateUserAsync(user);
                return true;
            }

            return false;
        }


        public async Task<bool> DeleteUserAsync(long userId)
        {
            User? user = await _userRepository.GetUserByUserIdAsync(userId);
            if (user != null)
            {
                await _userRepository.DeleteUserAsync(user);
                return true;
            }

            return false;
        }

        public async Task<string?> AuthenticateUserAsync(LoginDto loginDto)
        {
            string password = Util.GenerateHash(loginDto.Password);
            User? existingUser = await _userRepository.GetUserByEmailAsync(loginDto.Email);
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
