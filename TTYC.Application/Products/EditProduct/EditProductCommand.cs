using MediatR;

namespace TTYC.Application.Products.EditProduct
{
    public class EditProductCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
