using MediatR;

namespace TTYC.Application.Users.Commands.AddUser
{
	public class AddUserCommand : IRequest
	{
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
	}
}
