using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using TTYC.Application.Payment.Checkout;

namespace TTYC.ClientAPI.User
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ISender mediatr;

        public PaymentController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var items = await mediatr.Send(new CheckoutQuery());

            var options = new SessionCreateOptions
            {
                LineItems = items,
                Mode = "payment",
                SuccessUrl = "https://localhost:7001/",
                CancelUrl = "https://localhost:7001/",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Ok(session.Url);
        }
    }
}
