using ITPLibrary.Core.Dtos.UserDtos;
using ITPLibrary.Core.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var (user, errorMessage) = await _userService.RegisterUserAsync(registerUserDto);
            if (user == null)
            {
                return Conflict(new { message = errorMessage });
            }

            return CreatedAtAction(nameof(RegisterUser), new { id = user.Id }, user);
        }
    }
}
