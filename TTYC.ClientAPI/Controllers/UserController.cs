using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Users.AddUser;
using TTYC.Application.Users.BlockUser;
using TTYC.Application.Users.GetUsers;
using TTYC.Constants;

namespace TTYC.ClientAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ISender mediatr;

        public UserController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Registration.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] AddUserCommand command)
        {
            var response = await mediatr.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Blocks user by id for input hours.
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpPut]
        public async Task<IActionResult> BlockUser([FromBody] BlockUserCommand command)
        {
            var lockoutEnd = await mediatr.Send(command);
            return Ok(lockoutEnd);
        }

        /// <summary>
        /// Gets list of users.
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var query = new GetUsersQuery();
            var users = await mediatr.Send(query);
            return Ok(users);
        }
    }
}
