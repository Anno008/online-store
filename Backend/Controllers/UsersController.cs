using Microsoft.AspNetCore.Mvc;
using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Authorize()]
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        public UsersController(UserService userService) =>
            _userService = userService;

        [HttpPost]
        [ActionName("login")]
        public IActionResult Login(UserDTO user)
        {
            var result = _userService.Login(user.Username, user.Password);
            if (result == null)
                return BadRequest("Incorrect credentials.");

            return Ok(result);
        }

        [HttpPost]
        [ActionName("register")]
        public IActionResult Register(UserDTO user)
        {
            var result = _userService.Register(user.Username, user.Password);
            if (result != null)
                return Ok(result);

            return BadRequest("The user with the given username already exists.");
        }

     
    }
}
