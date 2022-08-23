using MediatR;
using TTYC.Application.Models;

namespace TTYC.Application.Users.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<IEnumerable<UserInfrastructure>>
    {
    }
}
