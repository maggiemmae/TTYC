using MediatR;

namespace TTYC.Application.Users.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<string>
    {
        public string PhoneNumber { get; set; }
    }
}
