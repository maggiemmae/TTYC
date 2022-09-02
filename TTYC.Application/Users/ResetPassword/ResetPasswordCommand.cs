using MediatR;

namespace TTYC.Application.Users.ResetPassword
{
    public class ResetPasswordCommand : IRequest
    {
        public string PhoneNumder { get; set; }
        public string RecoveryCode { get; set; }
        public string NewPassword { get; set; }
    }
}
