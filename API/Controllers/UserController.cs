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

        [HttpGet]
        public IActionResult Get(User user)
        {
            User foundUser = _userService.FindOneUser(user.id);
            return  foundUser != null ? new ObjectResult(foundUser) { StatusCode = StatusCodes.Status200OK } : new ObjectResult(foundUser) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpPost("search")]
        public IActionResult Search(User user)
        {
            List<User> users = _userService.FindUser(user);
            return users != null ? new ObjectResult(users) { StatusCode = StatusCodes.Status200OK } : new ObjectResult(users) { StatusCode = StatusCodes.Status400BadRequest }; ;
        }

        [HttpPost]
        public IActionResult Add([FromBody] User user)
        {
            User addedUser = _userService.AddUser(user);
            return addedUser != null ? new ObjectResult(addedUser) { StatusCode = StatusCodes.Status201Created } : new ObjectResult(addedUser) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] User user)
        {
            User deletedUser = _userService.DeleteUser(user);
            return deletedUser != null ? new ObjectResult(deletedUser) { StatusCode = StatusCodes.Status200OK } : new ObjectResult(deletedUser) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpPut]
        public IActionResult Update([FromBody] User user)
        {
            User updatedUser = _userService.UpdateUser(user);
            return updatedUser != null ? new ObjectResult(updatedUser) { StatusCode = StatusCodes.Status200OK } : new ObjectResult(updatedUser) { StatusCode = StatusCodes.Status400BadRequest };
        }
        
        [HttpGet("getAll")]
        public IActionResult getAll()
        {
            List<User> users = _userService.getAllUsers();
            return users != null ? new ObjectResult(users) { StatusCode = StatusCodes.Status200OK } : new ObjectResult(users) { StatusCode = StatusCodes.Status400BadRequest };
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
