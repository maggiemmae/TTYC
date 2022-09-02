using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Application.Interfaces;
using TTYC.Persistence;

namespace TTYC.Application.Users.AdminResetPassword
{
    public class AdminResetPasswordHandler : IRequestHandler<AdminResetPasswordCommand, Unit>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ISendRecoveryCodeService recoveryCodeService;

        public AdminResetPasswordHandler(ApplicationDbContext dbContext, ISendRecoveryCodeService sendRecoveryCode)
        {
            this.dbContext = dbContext;
            this.recoveryCodeService = sendRecoveryCode;
        }

        public async Task<Unit> Handle(AdminResetPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (user == null)
            {
                throw new NullReferenceException("User with such id not found");
            }

            user.IsPasswordReseted = true;
            dbContext.Update(user);
            await dbContext.SaveChangesAsync(cancellationToken);
            await recoveryCodeService.SendRecoveryCode(user.PhoneNumber);

            return Unit.Value;
        }
    }
}
