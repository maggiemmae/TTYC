using MediatR;

namespace TTYC.Application.Stores.DeleteStore
{
    public class DeleteStoreCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
