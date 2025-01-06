using System.Net;
using BackendStore.Dto.Request;
using BackendStore.Dto.Response;
using BackendStore.Helpers;
using BackendStore.Interface;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BackendStore.Controller
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequestDto userRequestDto)
        {
            try
            {
                UserResponseDto user = await _userRepository.RegisterAsync(userRequestDto);
                TemplateResponse<UserResponseDto> response = new TemplateResponse<UserResponseDto>(
                    "User Register Successfully",
                    HttpStatusCode.Created,
                    user
                );
                return base.Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                UserResponseDto user = await _userRepository.Login(loginRequestDto);
                string userId = user.Id;
                TemplateResponse<string> response = new TemplateResponse<string>(
                    "Login Successfully",
                    HttpStatusCode.Created,
                    userId
                );
                return base.Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                TemplateResponse<string> response = new TemplateResponse<string>(
                    "Internal Server Error",
                    HttpStatusCode.InternalServerError,
                    null
                );
                return BadRequest(response);
            }
        }
    }
}
