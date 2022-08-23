using MediatR;

namespace TTYC.Application.Stores.EditStore
{
    public class EditStoreCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
