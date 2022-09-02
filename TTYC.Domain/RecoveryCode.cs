namespace TTYC.Domain
{
    public class RecoveryCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime Expires { get; set; }
        public bool IsActivated { get; set; }

        public string PhoneNumder { get; set; }
        public virtual User User { get; set; }
    }
}
