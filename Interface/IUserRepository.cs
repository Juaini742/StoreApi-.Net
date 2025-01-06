using BackendStore.Dto.Request;
using BackendStore.Dto.Response;
using BackendStore.Model;

namespace BackendStore.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUser(string username);
        Task<UserResponseDto> RegisterAsync(UserRequestDto userRequestDto);
        Task<UserResponseDto> Login(LoginRequestDto loginRequestDto);
        User GetMe();
    }
}
