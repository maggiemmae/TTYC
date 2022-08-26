using MediatR;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Stores.GetStore;
using TTYC.Application.Stores.GetStoresList;

namespace TTYC.ClientAPI.User
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
