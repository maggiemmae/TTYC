using TTYC.Application.Interfaces;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Services
{
    public class DeliveryZoneCheckService : IDeliveryZoneCheckService
    {
        private readonly ApplicationDbContext dbContext;

        public DeliveryZoneCheckService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CheckDeliveryZone(double latitude, double longitude, Store store)
        {
            var distance = CalculateDistance(store.Latitude, store.Longitude, latitude, longitude);
            var zoneRadius = dbContext.DeliverySettings.Select(x => x.Radius).FirstOrDefault();
            return distance <= zoneRadius;
        }

        public double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            var result = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)) * 100;
            return result;
        }
    }
}
