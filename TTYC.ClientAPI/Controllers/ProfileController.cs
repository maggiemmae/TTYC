using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Users.AddProfile;
using TTYC.Application.Users.GetUserProfile;
using TTYC.Constants;

namespace TTYC.ClientAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ISender mediatr;

        public ProfileController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Adds user profile with address.
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddUserProfile([FromBody] AddProfileCommand command)
        {
            var profileId = await mediatr.Send(command);
            return Ok(profileId);
        }

        /// <summary>
        /// Gets user profile of authorized user.
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {
            var query = new GetUserProfileQuery();
            var profile = await mediatr.Send(query);
            return Ok(profile);
        }
    }
}
