using TTYC.Domain;

namespace TTYC.Application.Interfaces
{
    public interface IDeliveryZoneCheckService
    {
        bool CheckDeliveryZone(double latitude, double longtitude, Store store);
        double CalculateDistance(double x1, double y1, double x2, double y2);
    }
}
