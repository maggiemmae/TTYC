﻿namespace TTYC.Domain
{
	public class User
	{
		public Guid Id { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }

		public virtual UserProfile Profile { get; set; }
	}
}
