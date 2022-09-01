using MediatR;

namespace TTYC.Application.Adresses.DeliveryZoneCheck
{
    public class DeliveryZoneCheckQuery : IRequest<bool>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public DeliveryZoneCheckQuery(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
