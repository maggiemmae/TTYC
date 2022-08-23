using MediatR;
using TTYC.Domain;

namespace TTYC.Application.Users.Queries.ValidatePassword
{
    public class ValidatePasswordQuery : IRequest<User>
    {
        public string UserName { get; set; }
    }
}
