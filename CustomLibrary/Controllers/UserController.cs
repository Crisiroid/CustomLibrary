using CustomLibrary.Data;
using CustomLibrary.Interfaces;
using CustomLibrary.Models;
using CustomLibrary.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustomLibrary.Controllers
{
    [ApiController]
    [Route("/v1/Users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService; 
        }
        
        
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }
        
        
        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody]UserDTO user)
        {
            try
            {
                User findUser = _userService.FindUserByEmail(user.Email);
                if(findUser == null)
                {
                    var pass = _authService.HashPassword(user.Password); 
                    User newUser = new User
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = pass
                    };

                    _userService.CreateUser(newUser);

                    return Ok(new {Email = user.Email, password = pass});
                }
                else
                {
                    return BadRequest("Submitted Email is Available in Database");
                }
                
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                User user = _userService.FindUserById(id);
                if(user == null)
                {
                    return BadRequest("User is not Found!");
                }

                _userService.DeleteUser(user);
                return Ok("User is Deleted");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpPatch("UpdateUser")]
        public IActionResult UpdateUser(UserDTO user)
        {
            try
            {
                User findUser = _userService.FindUserByEmail(user.Email);
                if(findUser == null)
                {
                    return BadRequest("User not Found");
                }
                else
                {
                    findUser.Email = user.Email;
                    findUser.Name = user.Name;
                    findUser.Password = user.Password; 

                    _userService.UpdateUser(findUser);
                    return Ok("User Is Updated");
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }


    }
}
