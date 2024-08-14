using ITPLibrary.Api.Constants;
using ITPLibrary.Core.Dtos.UserDtos;
using ITPLibrary.Core.Services.IServices;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost($"{RouteConstants.Register}")]
        [AllowAnonymous]
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

        [HttpPost($"{RouteConstants.Login}")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var (response, errorMessage) = await _userService.LoginAsync(loginRequestDto);
            if (response == null)
            {
                return Unauthorized(new { message = errorMessage });
            }

            return Ok(response);
        }
    }
}
