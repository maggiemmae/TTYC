using MediatR;

namespace TTYC.Application.Stores.AddProductToStore
{
    public class AddProductToStoreCommand : IRequest
    {
        public Guid ProductId { get; set; }
        public Guid StoreId { get; set; }
    }
}
