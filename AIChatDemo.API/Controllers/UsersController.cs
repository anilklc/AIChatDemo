using AIChatDemo.API.DTOs;
using AIChatDemo.API.Interfaces;
using AIChatDemo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIChatDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUser)
        {
            var user = await _userService.Login(loginUser);
            return Ok(user);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUser)
        {
            var response = await _userService.CreateUser(createUser);
            return Ok(response);
        }


    }
}
