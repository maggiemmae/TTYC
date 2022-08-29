using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Application.Interfaces;
using TTYC.Constants;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Orders.AddOrder
{
    public class AddOrderHandler : IRequestHandler<AddOrderCommand, Guid>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public AddOrderHandler(ApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(AddOrderCommand command, CancellationToken cancellationToken)
        {
            var cartItems = await dbContext.CartItems
                    .Include(x => x.Product)
                    .Where(x => x.CartId == currentUserService.UserId)
                    .ToListAsync(cancellationToken);

            var totalSum = dbContext.CartItems
                   .Where(x => x.CartId == currentUserService.UserId)
                   .Select(x => x.Count * x.Product.Price).Sum();

            var order = new Order()
            {
                Id = Guid.NewGuid(),
                Status = OrderStatus.Created,
                CreatedDate = DateTime.UtcNow,
                TotalSum = totalSum,
                UserId = currentUserService.UserId,
                AddressId = command.AddressId,
                CartItems = cartItems
            };

            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
