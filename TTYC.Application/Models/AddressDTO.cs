namespace TTYC.Application.Models
{
    public class AddressDTO
    {
        public Guid Id;
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int FlatNumber { get; set; }
        public int Floor { get; set; }

        public AddressDTO() => Id = Guid.NewGuid();
    }
}
