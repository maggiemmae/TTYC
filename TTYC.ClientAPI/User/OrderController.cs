using MediatR;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Orders.AddOrder;

namespace TTYC.ClientAPI.User
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISender mediatr;

        public OrderController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Creates order.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateOrder(AddOrderCommand command)
        {
            var response = await mediatr.Send(command);
            return Ok(response);
        }
    }
}
