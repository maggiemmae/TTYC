using MediatR;
using TTYC.Domain;

namespace TTYC.Application.Products.GetProduct
{
    public class GetProductQuery : IRequest<Product>
    {
        public Guid Id { get; set; }
    }
}
