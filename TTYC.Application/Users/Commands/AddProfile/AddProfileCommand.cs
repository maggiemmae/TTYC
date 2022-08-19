using MediatR;

namespace TTYC.Application.Users.Commands.AddProfile
{
	public class AddProfileCommand : IRequest
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }

		public string Street { get; set; }
		public int HouseNumber { get; set; }
		public int FlatNumber { get; set; }
		public int Floor { get; set; }

		public string PhoneNumber { get; set; }
	}
}
