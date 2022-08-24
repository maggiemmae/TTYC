namespace TTYC.Application.Models
{
    public class UserInfrastructure
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public DateTime LockoutEnd { get; set; }
    }
}
