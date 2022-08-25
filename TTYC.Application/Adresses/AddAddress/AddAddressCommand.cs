using MediatR;

namespace TTYC.Application.Adresses.AddAddress
{
    public class AddAddressCommand : IRequest<Guid>
    {
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int FlatNumber { get; set; }
        public int Floor { get; set; }
    }
}
