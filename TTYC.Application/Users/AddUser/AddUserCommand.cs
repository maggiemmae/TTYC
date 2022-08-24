using MediatR;

namespace TTYC.Application.Users.AddUser
{
    public class AddUserCommand : IRequest<Guid>
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
