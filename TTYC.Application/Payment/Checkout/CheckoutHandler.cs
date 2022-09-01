using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using System.Data;
using TTYC.Application.Interfaces;
using TTYC.Constants;
using TTYC.Persistence;

namespace TTYC.Application.Payment.Checkout
{
    public class CheckoutHandler : IRequestHandler<CheckoutCommand, string>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;
        private readonly IMapper mapper;
        private readonly StripeOptions stripeOptions;

        public CheckoutHandler(ApplicationDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper, IOptions<StripeOptions> stripeOptions)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
            this.mapper = mapper;
            this.stripeOptions = stripeOptions.Value;
        }

        public async Task<string> Handle(CheckoutCommand command, CancellationToken cancellationToken)
        {
            var order = await dbContext.Orders
                .OrderBy(x => x.CreatedDate)
                .Include(x => x.CartItems)
                .LastOrDefaultAsync(x => x.UserId == currentUserService.UserId, cancellationToken);

            var items = mapper.Map<List<SessionLineItemOptions>>(order.CartItems);

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

            order.Status = OrderStatus.Unpaid;
            dbContext.Orders.Update(order);
            dbContext.CartItems.RemoveRange(order.CartItems);
            await dbContext.SaveChangesAsync(cancellationToken);

            return session.Url;
        }
    }
}
