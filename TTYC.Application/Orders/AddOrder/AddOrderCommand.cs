using MediatR;

namespace TTYC.Application.Orders.AddOrder
{
    public class AddOrderCommand : IRequest<Guid>
    {
        public Guid AddressId { get; set; }
    }
}
