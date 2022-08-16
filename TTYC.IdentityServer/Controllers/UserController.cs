using MediatR;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Users.Queries.GetUsers;

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

		[HttpGet]
		public async Task<IActionResult> GetUser()
		{
			var query = new GetUsersQuery();
			var responce = await mediatr.Send(query);
			return Ok(responce);
		}
	}
}