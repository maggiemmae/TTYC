using MediatR;
using TTYC.Application.Models;

namespace TTYC.Application.Users.GetUsers
{
    public class GetUsersQuery : IRequest<IEnumerable<UserInfrastructure>>
    {
    }
}
