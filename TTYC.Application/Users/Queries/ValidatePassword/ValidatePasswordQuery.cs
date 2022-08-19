using MediatR;

namespace TTYC.Application.Users.Queries.ValidatePassword
{
	public class ValidatePasswordQuery : IRequest<bool>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}
