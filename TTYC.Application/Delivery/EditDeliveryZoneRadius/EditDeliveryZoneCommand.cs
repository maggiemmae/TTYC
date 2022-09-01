using MediatR;

namespace TTYC.Application.Delivery.EditDeliveryZoneRadius
{
    public class EditDeliveryZoneCommand : IRequest
    {
        public double Radius { get; set; }
    }
}
