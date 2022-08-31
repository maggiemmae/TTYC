using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using TTYC.Application.Payment.Checkout;
using TTYC.Constants;

namespace TTYC.ClientAPI.User
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ISender mediatr;
        private readonly StripeOptions stripeOptions;

        public PaymentController(ISender mediatr, IOptions<StripeOptions> stripeOptions)
        {
            this.mediatr = mediatr;
            this.stripeOptions = stripeOptions.Value;
        }

        /// <summary>
        /// Get a link for chekout.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var items = await mediatr.Send(new CheckoutQuery());

            var options = new SessionCreateOptions
            {
                LineItems = items,
                Mode = "payment",
                SuccessUrl = stripeOptions.SuccessUrl,
                CancelUrl = stripeOptions.CancelUrl
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Ok(session.Url);
        }
    }
}
