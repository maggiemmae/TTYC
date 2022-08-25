using MediatR;

namespace TTYC.Application.Adresses.DeleteAddress
{
    public class DeleteAddressCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
