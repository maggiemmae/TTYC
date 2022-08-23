using MediatR;

namespace TTYC.Application.Users.Commands.BlockUser
{
    public class BlockUserCommand : IRequest<DateTime>
    {
        public Guid Id { get; set; }
        public int Hours { get; set; }
    }
}
