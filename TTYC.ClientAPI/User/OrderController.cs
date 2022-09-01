using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Orders.AddOrder;
using TTYC.Application.Orders.GetOrder;

namespace TTYC.ClientAPI.User
{
    [Authorize]
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

        /// <summary>
        /// Gets last order of current user.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            var response = await mediatr.Send(new GetOrderQuery());
            return Ok(response);
        }
    }
}
