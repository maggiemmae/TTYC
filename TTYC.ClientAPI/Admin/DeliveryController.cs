using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Delivery.EditDeliveryZoneRadius;
using TTYC.Constants;

namespace TTYC.ClientAPI.Admin
{
    [Authorize(Roles = Roles.Admin)]
    [Route("[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly ISender mediatr;

        public DeliveryController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Edits delivery zone radius.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> EditDeliveryZoneRadius([FromBody] EditDeliveryZoneCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }
    }
}
