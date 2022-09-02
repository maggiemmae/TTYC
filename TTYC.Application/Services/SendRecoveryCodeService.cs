using TTYC.Application.Interfaces;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Services
{
    public class SendRecoveryCodeService : ISendRecoveryCodeService
    {
        private readonly ApplicationDbContext dbContext;

        public SendRecoveryCodeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> SendRecoveryCode(string PhoneNumber)
        {
            var recoveryCode = new RecoveryCode
            {
                Id = Guid.NewGuid(),
                Code = GenerateRecoveryCode(4),
                PhoneNumder = PhoneNumber,
                Expires = DateTime.UtcNow.AddHours(1),
                IsActivated = false
            };

            dbContext.RecoveryCodes.Add(recoveryCode);
            await dbContext.SaveChangesAsync();

            return recoveryCode.Code;
        }

        public static string GenerateRecoveryCode(int length)
        {
            return new string(Guid.NewGuid().ToString()[..length]);
        }
    }
}
