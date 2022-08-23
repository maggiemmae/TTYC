using MediatR;
using TTYC.Domain;

namespace TTYC.Application.Users.Queries.GetUserProfile
{
    public class GetUserProfileQuery : IRequest<UserProfile>
    {
    }
}
