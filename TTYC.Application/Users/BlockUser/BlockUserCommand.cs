using MediatR;

namespace TTYC.Application.Users.BlockUser
{
    public class BlockUserCommand : IRequest<DateTime>
    {
        public Guid Id { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
    }
}
