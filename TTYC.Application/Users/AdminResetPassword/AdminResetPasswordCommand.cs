using MediatR;

namespace TTYC.Application.Users.AdminResetPassword
{
    public class AdminResetPasswordCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
