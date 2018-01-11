using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.DTOs;
using Backend.Services;
using System.Net.Http;
using System.Net;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService) =>
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

        [HttpGet]
        [ActionName("test")]
        public IActionResult Test()
        {
            //return "Super secret content, I hope you've got clearance for this...";
            return StatusCode(401, "What are you");
        }
    }
}
