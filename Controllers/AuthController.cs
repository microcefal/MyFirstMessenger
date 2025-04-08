using Microsoft.AspNetCore.Mvc;
using Messenger.IServices;
using Messenger.Models.DTO;

namespace Messenger.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = await _authService.Register(dto.Username, dto.Password);
            if (user == null)
            {
                return BadRequest("Пользователь уже существует");
            }
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login( [FromBody ]LoginDto dto)
        {
            
            if (dto == null)
            {
                return BadRequest("Некорректные данные");
            }
            var token = await _authService.Login(dto.Username,dto.Password);
            if (token == null)
            {
                return BadRequest("Неверные данные");
            }
            return Ok(new {Toketn = token});

        }
    }

}