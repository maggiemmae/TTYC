namespace TTYC.Application.Interfaces
{
    public interface ISendRecoveryCodeService
    {
        Task<string> SendRecoveryCode(string PhoneNumber);
    }
}
