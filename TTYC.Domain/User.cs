namespace TTYC.Domain
{
	public class User
	{
		public Guid UserId { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }

		public UserProfile Profile { get; set; }
	}
}
