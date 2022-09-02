using MediatR;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Users.ResendRecoveryCode;
using TTYC.Application.Users.ResetPassword;

namespace TTYC.ClientAPI.User
{
    [Route("[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly ISender mediatr;

        public PasswordController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Resets user password.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }

        /// <summary>
        /// Resends recovery code.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ResendRecoveryCode([FromQuery] ResendRecoveryCodeCommand command)
        {
            var response = await mediatr.Send(command);
            return Ok(response);
        }
    }
}
