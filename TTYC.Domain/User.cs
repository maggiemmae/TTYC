namespace TTYC.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime LockoutEnd { get; set; }
        public bool IsPasswordReseted { get; set; }

        public virtual IList<RecoveryCode> RecoveryCodes { get; set; }  
        public virtual UserProfile Profile { get; set; }
    }
}
