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
        private readonly IDeliveryZoneCheckService deliveryZoneCheck;

        public AddOrderHandler(ApplicationDbContext dbContext, ICurrentUserService currentUserService, IDeliveryZoneCheckService deliveryZoneCheck)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
            this.deliveryZoneCheck = deliveryZoneCheck;
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

            var products = cartItems.Select(x => x.Product).ToList();
            var address = await dbContext.Addresses
                .FirstOrDefaultAsync(x => x.Id == command.AddressId, cancellationToken);
            var stores = await dbContext.Stores
                .Include(x => x.Products).ToListAsync(cancellationToken);
            var zoneRadius = await dbContext.DeliverySettings
                .Select(x => x.Radius).FirstOrDefaultAsync(cancellationToken);

            var productsStore = new Dictionary<Guid, double>();
            foreach (var store in stores)
            {
                if (products.All(x => store.Products.Contains(x)))
                {
                    var distance = deliveryZoneCheck.CalculateDistance
                        (address.Latitude, address.Longitude, store.Latitude, store.Longitude);
                    productsStore.Add(store.Id, distance);
                }
            }

            if(productsStore == null)
            {
                throw new Exception("One of your products is out of stock");
            }
            var nearestStore = productsStore.Values.Min();
            if (nearestStore > zoneRadius)
            {
                throw new Exception("Your address isn't in delivery zone");
            }

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
