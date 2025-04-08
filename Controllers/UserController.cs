using Messenger.IServices;
using Messenger.Models;
using Messenger.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync()
        {
            var users = await _userService.GetAllAsync();

            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
            }).ToList(); 
        return Ok(userDtos);
        }







        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

            return Ok(userDto);
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        //{
        //    var user = new User
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = dto.UserName,
        //        Email = dto.Email,
        //        CreatedDate = DateTime.UtcNow
        //    };
        //    await _userService.AddAsync(user);
        //    return Ok(user);
        //}
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User updateuser)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Name = updateuser.Name;
            user.Email = updateuser.Email;

            await _userService.UpdateAsync(user);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
