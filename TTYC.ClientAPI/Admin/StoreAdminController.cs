using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Stores.AddStore;
using TTYC.Application.Stores.DeleteStore;
using TTYC.Application.Stores.EditStore;
using TTYC.Constants;

namespace TTYC.ClientAPI.Admin
{
    [Authorize(Roles = Roles.Admin)]
    [Route("[controller]")]
    [ApiController]
    public class StoreAdminController : ControllerBase
    {
        private readonly ISender mediatr;

        public StoreAdminController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Adds store.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddStore([FromBody] AddStoreCommand command)
        {
            var response = await mediatr.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Deletes store by id.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteStore([FromQuery] DeleteStoreCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }

        /// <summary>
        /// Edits store by id.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> EditStore([FromBody] EditStoreCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }
    }
}
