using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Payment.Checkout;

namespace TTYC.ClientAPI.User
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ISender mediatr;

        public PaymentController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Get a link for chekout.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var response = await mediatr.Send(new CheckoutCommand());
            return Ok(response);
        }
    }
}
