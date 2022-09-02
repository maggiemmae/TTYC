using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Persistence;

namespace TTYC.Application.Users.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly ApplicationDbContext dbContext;

        public ResetPasswordHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            var recoveryCode = await dbContext.RecoveryCodes
                .Include(x => x.User)
                .OrderBy(x => x.Expires)
                .LastOrDefaultAsync(x => x.PhoneNumder == command.PhoneNumder, cancellationToken);

            if (recoveryCode.Code != command.RecoveryCode || recoveryCode.Expires < DateTime.UtcNow || recoveryCode.IsActivated)
            {
                throw new Exception("Wrong recovery code or code is expired");
            }

            //var user = await dbContext.Users
            //    .FirstOrDefaultAsync(x => x.PhoneNumber == command.PhoneNumder, cancellationToken);
            var user = recoveryCode.User;
            user.Password = PasswordHelper.HashPassword(command.NewPassword);
            user.IsPasswordReseted = false;
            recoveryCode.IsActivated = true;

            dbContext.UpdateRange(user, recoveryCode);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
