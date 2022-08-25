using MediatR;
using TTYC.Application.Models;

namespace TTYC.Application.Products.GetProduct
{
    public class GetProductQuery : IRequest<ProductInfrastructure>
    {
        public Guid Id { get; set; }
    }
}
