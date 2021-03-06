using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebAPIHuisdierMonitor.Model;


namespace WebAPIHuisdierMonitor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly static User StaticUser = new User();

        [HttpPost("Post")]
        public IActionResult AddUser([FromBody] User user)
        {
            try
            {
                StaticUser.AddUser(user);
                return Ok();
            }
            catch (DivideByZeroException) // sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException) // User staat al in database
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
        }

        [HttpPost("Delete")]
        public IActionResult DeleteUser([FromBody] User user)
        {
            try
            {
                StaticUser.DeleteUser(user);
                return Ok();
            }
            catch (DivideByZeroException) // sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException) // ID niet in database
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        [HttpPost("Login")]
        public IActionResult ValidateLogIn([FromBody] User user)
        {
            try
            {
                int UserID = StaticUser.ValidateLogIn(user.UserName, user.PassWordHash);
                if (UserID != 0)
                {
                    return Ok(UserID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
            }
            catch (DivideByZeroException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("Username")]
        public IActionResult UpdateUsername([FromBody] User user)
        {
            try
            {
                StaticUser.UpdateUserName(user);
                return Ok();
            }
            catch (DivideByZeroException) // sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException) // User staat al in database
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
        }

        [HttpPut("Password")]
        public IActionResult UpdatePassword([FromBody] User user)
        {
            try
            {
                StaticUser.UpdatePassword(user);
                return Ok();
            }
            catch (DivideByZeroException) // sql error
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException) // User staat al in database
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

        }
    }
}
