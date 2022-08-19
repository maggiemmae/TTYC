namespace TTYC.Domain
{
	public class UserProfile
	{
		public Guid UserId { get; set; }

		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }

		public User User { get; set; }

		public IList<Address> Addresses { get; set; }
	}
}
