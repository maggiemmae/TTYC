using MediatR;
using Stripe.Checkout;
using TTYC.Application.Models;

namespace TTYC.Application.Payment.Checkout
{
    public class CheckoutQuery : IRequest<List<SessionLineItemOptions>>
    {
    }
}
