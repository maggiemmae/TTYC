using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Stores.AddProductToStore;
using TTYC.Constants;

namespace TTYC.ClientAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly ISender mediatr;

        public ManagementController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Adds product to store.
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddProductToStore([FromBody] AddProductToStoreCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }
    }
}
