using MediatR;
using TTYC.Domain;

namespace TTYC.Application.Orders.GetOrder
{
    public class GetOrderQuery : IRequest<Order>
    {
    }
}
