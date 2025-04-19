using GestionProductos.DTOs.User;
using GestionProductos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _service.RegisterAsync(dto);
            if (token == null)
                return BadRequest("User already exists");
            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _service.LoginAsync(dto);
            if (token == null)
                return BadRequest("Invalid credentials");
            return Ok(token);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _service.GetAllAsyn();
            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _service.RefreshToken(dto);
            if (token == null)
                return Unauthorized("Refresh token invalid or expired");

            return Ok(token);
        }
    }
}
