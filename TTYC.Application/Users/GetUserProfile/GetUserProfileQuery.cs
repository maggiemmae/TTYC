using MediatR;
using TTYC.Domain;

namespace TTYC.Application.Users.GetUserProfile
{
    public class GetUserProfileQuery : IRequest<UserProfile>
    {
    }
}
