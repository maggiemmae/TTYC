using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Application.Interfaces;
using TTYC.Persistence;

namespace TTYC.Application.Users.ForgotPassword
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, string>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ISendRecoveryCodeService recoveryCodeService;

        public ForgotPasswordHandler(ApplicationDbContext dbContext, ISendRecoveryCodeService recoveryCodeService)
        {
            this.dbContext = dbContext;
            this.recoveryCodeService = recoveryCodeService;
        }

        public async Task<string> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(x => x.PhoneNumber == command.PhoneNumber, cancellationToken);
            
            if (user == null)
            {
                throw new NullReferenceException("User with such phone number not found");
            }

            user.IsPasswordReseted = true;
            dbContext.Update(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return await recoveryCodeService.SendRecoveryCode(command.PhoneNumber); ;
        }
    }
}
