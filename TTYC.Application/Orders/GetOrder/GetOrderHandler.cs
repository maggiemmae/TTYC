using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Application.Interfaces;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Orders.GetOrder
{
    public class GetOrderHandler : IRequestHandler<GetOrderQuery, Order>
    {
        public readonly ApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public GetOrderHandler(ApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }

        public async Task<Order> Handle(GetOrderQuery query, CancellationToken cancellationToken)
        {
            var order = await dbContext.Orders
                .OrderBy(x => x.CreatedDate)
                .Include(x => x.CartItems)
                .LastOrDefaultAsync(x => x.UserId == currentUserService.UserId, cancellationToken);
            return order;
        }
    }
}
