using MediatR;
using TTYC.Application.Models;

namespace TTYC.Application.Carts.GetCart
{
    public class GetCartQuery : IRequest<ViewCart>
    {
    }
}
