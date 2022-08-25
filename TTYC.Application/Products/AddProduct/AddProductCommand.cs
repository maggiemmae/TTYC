using MediatR;

namespace TTYC.Application.Products.AddProduct
{
    public class AddProductCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IList<Guid> StoreIds { get; set; }
    }
}
