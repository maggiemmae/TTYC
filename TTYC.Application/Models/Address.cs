namespace TTYC.Application.Models
{
    public class Address
    {
        public Guid Id;
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int FlatNumber { get; set; }
        public int Floor { get; set; }
    }
}
