namespace TTYC.Application.Users.Models
{
	public class AddProfileModel
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }

		public string Street { get; set; }
		public int HouseNumber { get; set; }
		public int FlatNumber { get; set; }
		public int Floor { get; set; }
	}
}
