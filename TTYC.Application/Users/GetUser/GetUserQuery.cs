using MediatR;
using TTYC.Domain;

namespace TTYC.Application.Users.GetUser
{
    public class GetUserQuery : IRequest<User>
    {
        public string UserName { get; set; }
    }
}
