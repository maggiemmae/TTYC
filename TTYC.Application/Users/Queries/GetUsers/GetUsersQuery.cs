using MediatR;
using TTYC.Domain;

namespace TTYC.Application.Users.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<IEnumerable<User>>
    {
    }
}
