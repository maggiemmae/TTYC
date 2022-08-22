namespace TTYC.Domain
{
	public class UserProfile
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }

		public Guid Id { get; set; }
		public virtual User User { get; set; }

		public IList<Address> Addresses { get; set; }
	}
}
