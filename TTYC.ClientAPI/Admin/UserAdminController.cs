using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Users.BlockUser;
using TTYC.Application.Users.GetUsers;
using TTYC.Constants;

namespace TTYC.ClientAPI.Admin
{
    [Authorize(Roles = Roles.Admin)]
    [Route("[controller]")]
    [ApiController]
    public class UserAdminController : ControllerBase
    {
        private readonly ISender mediatr;

        public UserAdminController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Blocks user by id for input hours.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> BlockUser([FromBody] BlockUserCommand command)
        {
            var lockoutEnd = await mediatr.Send(command);
            return Ok(lockoutEnd);
        }

        /// <summary>
        /// Gets list of users.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var query = new GetUsersQuery();
            var users = await mediatr.Send(query);
            return Ok(users);
        }
    }
}
