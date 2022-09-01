using MediatR;
using TTYC.Constants;

namespace TTYC.Application.Orders.EditStatus
{
    public class EditStatusCommand : IRequest
    {
        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
