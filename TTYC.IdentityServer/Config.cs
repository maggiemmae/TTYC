using IdentityServer4.Models;
using TTYC.Constants;

namespace TTYC.IdentityServer
{
	public static class Config
	{
		public static ClientOptions ClientOptions { get; set; } = new ClientOptions();

		public static IEnumerable<ApiScope> ApiScopes =>
			new List<ApiScope>
			{
				new ApiScope("ClientAPI")
			};

		public static IEnumerable<IdentityResource> IdentityResources =>
			new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
                new IdentityResource("roles", "User role", new List<string> { "role" })
            };

		public static IEnumerable<Client> Clients =>
			new List<Client>
			{
				new Client
				{
					ClientId = ClientOptions.ClientId,
					AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
					ClientSecrets = {new Secret("secret".Sha256())},
					AllowedScopes = {"ClientAPI", "offline_access", "roles"},
					AllowOfflineAccess = true,
					AccessTokenLifetime = ClientOptions.AccessTokenLifetime
				}
			};
	}
}
