using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Users.AdminResetPassword;
using TTYC.Constants;

namespace TTYC.ClientAPI.Admin
{
    [Authorize(Roles = Roles.Admin)]
    [Route("[controller]")]
    [ApiController]
    public class PasswordAdminController : ControllerBase
    {
        private readonly ISender mediatr;

        public PasswordAdminController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Reset password for chosen user.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> ResetPasswordAdmin([FromBody] AdminResetPasswordCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }
    }
}
