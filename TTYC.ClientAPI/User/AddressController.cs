using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Adresses.AddAddress;
using TTYC.Application.Adresses.DeleteAddress;
using TTYC.Application.Adresses.DeliveryZoneCheck;
using TTYC.Application.Adresses.EditAddress;
using TTYC.Application.Interfaces;

namespace TTYC.ClientAPI.User
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ISender mediatr;

        public AddressController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Adds new address.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddAddress(AddAddressCommand command)
        {
            var response = await mediatr.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Deletes address.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteAddress(DeleteAddressCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }

        /// <summary>
        /// Updates address.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> EditAddress(EditAddressCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }

        /// <summary>
        /// Delivery zone check.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> DeliveryZoneCheck(double latitude, double longitude)
        {
            var result = await mediatr.Send(new DeliveryZoneCheckQuery(latitude, longitude));
            return Ok(result);
        }
    }
}
