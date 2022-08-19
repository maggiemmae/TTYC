using MediatR;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Users.Commands.AddUser;

namespace TTYC.IdentityServer.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly ISender mediatr;

		public AccountController(ISender mediatr)
		{
			this.mediatr = mediatr;
		}

		/// <summary>
		/// Registration.
		/// </summary>
		[HttpPost]
		public async Task<IActionResult> SignUp([FromBody] AddUserCommand command)
		{
			var responce = await mediatr.Send(command);
			return Ok(responce);
		}
	}
}
