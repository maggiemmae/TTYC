using MediatR;
using TTYC.Domain;

namespace TTYC.Application.Stores.GetStore
{
    public class GetStoreQuery : IRequest<Store>
    {
        public Guid Id { get; set; }
    }
}
