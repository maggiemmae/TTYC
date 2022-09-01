using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using TTYC.Application.Orders.EditStatus;
using TTYC.Constants;

namespace TTYC.ClientAPI.User
{
    [Route("webhook")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        private readonly ISender mediatr;
        private readonly StripeOptions stripeOptions;

        public WebhookController(ISender mediatr, IOptions<StripeOptions> stripeOptions)
        {
            this.mediatr = mediatr;
            this.stripeOptions = stripeOptions.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                  json,
                  Request.Headers[PaymentOptions.Signature],
                  stripeOptions.WebhookSecret
                );

                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;

                    var service = new PaymentIntentService();
                    var payment = service.Get(session.PaymentIntentId);
                    var id = Guid.Parse(payment.Metadata[PaymentOptions.OrderId]);

                    await mediatr.Send(new EditStatusCommand { Id = id, OrderStatus = OrderStatus.Paid });
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }

    }
}
