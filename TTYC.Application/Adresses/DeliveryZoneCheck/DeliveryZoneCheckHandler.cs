using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Application.Interfaces;
using TTYC.Persistence;

namespace TTYC.Application.Adresses.DeliveryZoneCheck
{
    public class DeliveryZoneCheckHandler : IRequestHandler<DeliveryZoneCheckQuery, bool>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDeliveryZoneCheckService deliveryZoneCheck;

        public DeliveryZoneCheckHandler(ApplicationDbContext dbContext, IDeliveryZoneCheckService deliveryZoneCheck)
        {
            this.dbContext = dbContext;
            this.deliveryZoneCheck = deliveryZoneCheck;
        }

        public async Task<bool> Handle(DeliveryZoneCheckQuery query, CancellationToken cancellationToken)
        {
            var stores = await dbContext.Stores.ToListAsync(cancellationToken);
            foreach (var store in stores)
            {
                if (deliveryZoneCheck.CheckDeliveryZone(query.Latitude, query.Longitude, store))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
