using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Stores.AddStore;
using TTYC.Application.Stores.DeleteStore;
using TTYC.Application.Stores.EditStore;
using TTYC.Application.Stores.GetStore;
using TTYC.Application.Stores.GetStoresList;
using TTYC.Constants;

namespace TTYC.ClientAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly ISender mediatr;

        public StoreController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Adds store.
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddStore([FromBody] AddStoreCommand command)
        {
            var response = await mediatr.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Deletes store by id.
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteStore([FromQuery] DeleteStoreCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }

        /// <summary>
        /// Edits store by id.
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpPut]
        public async Task<IActionResult> EditStore([FromBody] EditStoreCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }

        /// <summary>
        /// Gets store by id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStore([FromRoute] Guid id)
        {
            var query = new GetStoreQuery()
            {
                Id = id
            };
            var store = await mediatr.Send(query);
            return Ok(store);
        }

        /// <summary>
        /// Gets paged list of stores.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetStoresList([FromQuery] GetStoresListQuery query)
        {
            var stores = await mediatr.Send(query);
            return Ok(stores);
        }
    }
}
