using IdentityServer4.Models;

namespace TTYC.IdentityServer
{
	public static class Config
	{
		public static IEnumerable<ApiScope> ApiScopes =>
			new List<ApiScope>
			{
				new ApiScope("ClientAPI")
			};

		public static IEnumerable<IdentityResource> IdentityResources =>
			new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile()
			};

		public static IEnumerable<Client> Clients =>
			new List<Client>
			{
				new Client
				{
					ClientId = "clientAPI",
					AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
					ClientSecrets = {new Secret("secret".Sha256())},
					AllowedScopes = {"ClientAPI", "offline_access"},
					AllowOfflineAccess = true,
					AccessTokenLifetime = 300
				}
			};
	}
}
