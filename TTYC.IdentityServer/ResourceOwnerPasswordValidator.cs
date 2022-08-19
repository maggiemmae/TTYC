using IdentityServer4.Models;
using IdentityServer4.Validation;
using MediatR;
using TTYC.Application.Users.Queries.ValidatePassword;

namespace TTYC.IdentityServer
{
	public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
	{
		private readonly ISender mediatr;

		public ResourceOwnerPasswordValidator(ISender mediatr)
		{
			this.mediatr = mediatr;
		}

		public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
		{
			var query = new ValidatePasswordQuery()
			{
				UserName = context.UserName,
				Password = context.Password
			};

			var isMatch = await mediatr.Send(query);

			if (isMatch) {
				context.Result = new GrantValidationResult(context.UserName, GrantType.ResourceOwnerPassword);
			}

			return;
		}
	}
}
