using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using TTYC.Application.Orders.EditStatus;
using TTYC.Application.Orders.GetOrder;
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
            var order = await mediatr.Send(new GetOrderQuery());

            var options = new SessionCreateOptions
            {
                LineItems = items,
                Mode = PaymentOptions.Mode,
                SuccessUrl = stripeOptions.SuccessUrl,
                CancelUrl = stripeOptions.CancelUrl,
                PaymentIntentData = new SessionPaymentIntentDataOptions
                {
                    Metadata =
                    new Dictionary<string, string> { { PaymentOptions.OrderId, order.Id.ToString() } }
                }
            };

            var service = new SessionService();
            Session session = service.Create(options);

            await mediatr.Send(new EditStatusCommand { Id = order.Id, OrderStatus = OrderStatus.Unpaid });

            return Ok(session.Url);
        }
    }
}
