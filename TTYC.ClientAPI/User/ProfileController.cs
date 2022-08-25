using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Products.GetProduct;
using TTYC.Application.Users.AddProfile;
using TTYC.Application.Users.GetUserProfile;

namespace TTYC.ClientAPI.User
{
    [Authorize]
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
        /// Adds profile.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddProfile(AddProfileCommand command)
        {
            var response = await mediatr.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Gets profile.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var query = new GetUserProfileQuery();
            return Ok(await mediatr.Send(query));
        }
    }
}
