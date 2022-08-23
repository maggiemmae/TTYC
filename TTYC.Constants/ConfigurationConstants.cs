namespace TTYC.Constants
{
	public static class ConfigurationConstants
	{
		public const string DefaultConnection = "DefaultConnection";

		public struct PasswordHashParams
		{
            public const int IterCount = 10000;
            public const int SubkeyLength = 256 / 8;
            public const int SaltSize = 128 / 8;
        }
	}
}