using MediatR;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Users.AddUser;
using TTYC.Application.Users.ForgotPassword;

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

        /// <summary>
        /// Get recovery code.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            var response = await mediatr.Send(command);
            return Ok(response);
        }
    }
}
