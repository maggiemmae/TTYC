using MediatR;

namespace TTYC.Application.Users.BlockUser
{
    public class BlockUserCommand : IRequest<DateTime>
    {
        public Guid Id { get; set; }
        public DateTime LockoutEnd { get; set; }
    }
}
