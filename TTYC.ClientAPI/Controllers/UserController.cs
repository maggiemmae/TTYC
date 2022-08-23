using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Models;
using TTYC.Application.Users.Commands.AddUser;
using TTYC.Application.Users.Commands.BlockUser;
using TTYC.Application.Users.Queries.GetUsers;
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
        public async Task<DateTime> BlockUser([FromBody] BlockUserCommand command)
        {
            return await mediatr.Send(command);
        }

        /// <summary>
        /// Gets list of users.
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IEnumerable<UserInfrastructure>> GetUsers()
        {
            var query = new GetUsersQuery();
            return await mediatr.Send(query);
        }
    }
}