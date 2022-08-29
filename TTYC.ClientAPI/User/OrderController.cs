using MediatR;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Orders.AddOrder;
using TTYC.Application.Payment.Checkout;

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

        /// <summary>
        /// Gets order.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Chekout()
        {
            var response = await mediatr.Send(new CheckoutQuery());
            return Ok(response);
        }
    }
}
