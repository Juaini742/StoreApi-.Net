using BackendStore.Data;
using BackendStore.Dto.Request;
using BackendStore.Dto.Respone;
using BackendStore.Interface;
using BackendStore.Model;
using Microsoft.EntityFrameworkCore;

namespace BackendStore.Repository
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _context;

        public UserRepository (AppDbContext context)
        {
            _context = context;
        }

        public User GetMe()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUser(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null) {
                throw new InvalidOperationException("User not found");
            }

            return user;
        }

        public async Task<UserResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var exisingUser = await GetUser(loginRequestDto.Username);

            var passwordVerify = BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, exisingUser.Password);
            if(!passwordVerify)
            {
                throw new InvalidOperationException("Invalid Username or Password");
            }

            return UserResponseDto.ToUserResponseDto(exisingUser);
        }

        public async Task<UserResponseDto> RegisterAsync(UserRequestDto userRequestDto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == userRequestDto.Username);

            if (existingUser != null) {
                throw new InvalidOperationException("Username already exist");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRequestDto.Password);

            User user = new User
            {
                Username = userRequestDto.Username,
                FirstName = userRequestDto.FirstName,
                LastName  = userRequestDto.LastName,
                Password = hashedPassword,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return UserResponseDto.ToUserResponseDto(user);
        }
    }
}
