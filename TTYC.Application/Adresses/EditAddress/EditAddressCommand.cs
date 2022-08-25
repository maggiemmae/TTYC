using MediatR;

namespace TTYC.Application.Adresses.EditAddress
{
    public class EditAddressCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int FlatNumber { get; set; }
        public int Floor { get; set; }
        public bool IsDefault { get; set; }
    }
}
