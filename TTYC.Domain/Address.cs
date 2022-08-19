namespace TTYC.Domain
{
	public class Address
	{
		public Guid AddressId { get; set; }
		public string Street { get; set; }
		public int HouseNumber { get; set; }
		public int FlatNumber { get; set; }
		public int Floor { get; set; }

		public Guid UserId { get; set; }
		public UserProfile Profile { get; set; }
	}
}
