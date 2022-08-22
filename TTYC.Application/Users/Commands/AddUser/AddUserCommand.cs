using MediatR;

namespace TTYC.Application.Users.Commands.AddUser
{
	public class AddUserCommand : IRequest<Guid>
	{
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
	}
}
