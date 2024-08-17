using CustomLibrary.Interfaces;
using CustomLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomLibrary.Controllers
{
    [Route("/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService; 
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpGet("login")]
        public IActionResult AuthenticateUser(string email,  string password, string deviceId, string ipAddress, string userId)
        {
            try
            {
                var hashedPassword = _authService.HashPassword(password);
                User user = _userService.FindUserByEmailAndPassword(email, hashedPassword);
                Console.WriteLine(hashedPassword);
                if(user == null)
                {
                    return NotFound();
                }
                var token = _authService.GenerateToken(hashedPassword, deviceId, ipAddress);
                _authService.StoreToken(userId, token, deviceId, ipAddress);
                return Ok(new {Token = token});

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        } 
    }
}
