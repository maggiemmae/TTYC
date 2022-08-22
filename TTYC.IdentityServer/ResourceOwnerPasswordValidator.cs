﻿using IdentityServer4.Models;
using IdentityServer4.Validation;
using MediatR;
using TTYC.Application;
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
				UserName = context.UserName
			};

			var user = await mediatr.Send(query);
            var isMatch = PasswordHelper.VerifyHashedPassword(user.Password, context.Password);

            if (isMatch) {
				context.Result = new GrantValidationResult(user.Id.ToString(), GrantType.ResourceOwnerPassword);
			}

			return;
		}
	}
}