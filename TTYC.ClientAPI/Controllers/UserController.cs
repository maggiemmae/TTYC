using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Users.Commands.AddProfile;
using TTYC.Application.Users.Models;
using TTYC.Application.Users.Queries.GetUserProfile;
using TTYC.Domain;

namespace TTYC.IdentityServer.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private readonly ISender mediatr;

		public UserController(ISender mediatr)
		{
			this.mediatr = mediatr;
		}

		/// <summary>
		/// Adding user profile with address.
		/// </summary>
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> AddUserProfile([FromBody] AddProfileModel model)
		{
			var command = new AddProfileCommand()
			{
				Name = model.Name,
				Surname = model.Surname,
				Email = model.Email,
				PhoneNumber = User.FindFirst("sub").Value,
				FlatNumber = model.FlatNumber,
				Street = model.Street,
				HouseNumber = model.HouseNumber,
				Floor = model.Floor
			};

			await mediatr.Send(command);
			return Ok();
		}

		/// <summary>
		/// Gets user profile of authorized user.
		/// </summary>
		[Authorize]
		[HttpGet]
		public async Task<UserProfile> GetUserProfile()
		{
			var query = new GetUserProfileQuery()
			{
				PhoneNumber = User.FindFirst("sub").Value
			};

			return await mediatr.Send(query);
		}
	}
}