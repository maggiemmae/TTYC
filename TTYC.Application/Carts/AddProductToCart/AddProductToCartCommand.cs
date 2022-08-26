using MediatR;

namespace TTYC.Application.Carts.AddProductToCart
{
    public class AddProductToCartCommand : IRequest
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; } = 1;
    }
}
