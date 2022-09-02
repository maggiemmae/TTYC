using MediatR;
using TTYC.Application.Interfaces;

namespace TTYC.Application.Users.ResendRecoveryCode
{
    public class ResendRecoveryCodeHandler : IRequestHandler<ResendRecoveryCodeCommand, string>
    {
        private readonly ISendRecoveryCodeService recoveryCodeService;

        public ResendRecoveryCodeHandler(ISendRecoveryCodeService recoveryCodeService)
        {
            this.recoveryCodeService = recoveryCodeService;
        }

        public async Task<string> Handle(ResendRecoveryCodeCommand command, CancellationToken cancellationToken)
        {
            return await recoveryCodeService.SendRecoveryCode(command.PhoneNumber);
        }
    }
}
