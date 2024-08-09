using CustomLibrary.Data;
using CustomLibrary.Models;
using CustomLibrary.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustomLibrary.Controllers
{
    [ApiController]
    [Route("/v1/Users")]
    public class UserController : ControllerBase
    {
        private readonly LibraryDBContext _dbContext;
        public UserController(LibraryDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            return Ok(_dbContext.Users.ToList());
        }
        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody]UserDTO user)
        {
            try
            {
                User newUser = new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password
                };
                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();

                return Ok("User Was Created");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

    }
}
