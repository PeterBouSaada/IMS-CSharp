using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("API/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("search")]
        public List<User> Get(User user)
        {
            return _userService.FindUser(user);
        }

        [HttpPost]
        public IActionResult Add([FromBody] User user)
        {
            return _userService.AddUser(user) != null ? new ObjectResult(user) { StatusCode = StatusCodes.Status201Created } : new ObjectResult(user) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] User user)
        {
            return _userService.DeleteUser(user) != null ? new ObjectResult(user) { StatusCode = StatusCodes.Status200OK } : new ObjectResult(user) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpPut]
        public IActionResult Update([FromBody] User user)
        {
            return _userService.UpdateUser(user) != null ? new ObjectResult(user) { StatusCode = StatusCodes.Status200OK } : new ObjectResult(user) { StatusCode = StatusCodes.Status400BadRequest };

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User user)
        {
            string token = _userService.AuthenticateUser(user);
            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

    }
}
