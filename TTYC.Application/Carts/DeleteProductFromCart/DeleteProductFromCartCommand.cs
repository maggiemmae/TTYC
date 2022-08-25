using MediatR;

namespace TTYC.Application.Carts.DeleteProductFromCart
{
    public class DeleteProductFromCartCommand : IRequest
    {
        public Guid ProductId { get; set; }
    }
}
