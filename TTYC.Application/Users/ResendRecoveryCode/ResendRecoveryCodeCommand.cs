using MediatR;

namespace TTYC.Application.Users.ResendRecoveryCode
{
    public class ResendRecoveryCodeCommand : IRequest<string>
    {
        public string PhoneNumber { get; set; }
    }
}
