using MediatR;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Users.AddUser;

namespace TTYC.ClientAPI.User
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISender mediatr;

        public AccountController(ISender mediatr)
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
    }
}
