using MediatR;
using TTYC.Application.Models;

namespace TTYC.Application.Orders.GetOrder
{
    public class GetOrderQuery : IRequest<OrderInfrastructure>
    {
    }
}
