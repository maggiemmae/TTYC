using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using System.Data;
using TTYC.Application.Interfaces;
using TTYC.Persistence;

namespace TTYC.Application.Payment.Checkout
{
    public class CheckoutHandler : IRequestHandler<CheckoutQuery, List<SessionLineItemOptions>>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;
        private readonly IMapper mapper;

        public CheckoutHandler(ApplicationDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
            this.mapper = mapper;
        }

        public async Task<List<SessionLineItemOptions>> Handle(CheckoutQuery query, CancellationToken cancellationToken)
        {
            var order = await dbContext.Orders
                .OrderBy(x => x.CreatedDate)
                .Include(x => x.CartItems)
                .LastOrDefaultAsync(x => x.UserId == currentUserService.UserId, cancellationToken);
            
            var response = mapper.Map<List<SessionLineItemOptions>>(order.CartItems);
            dbContext.CartItems.RemoveRange(order.CartItems);
            await dbContext.SaveChangesAsync(cancellationToken);
            return response;
        }
    }
}
