using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Application.Interfaces;
using TTYC.Application.Services;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Users.Queries.GetUserProfile
{
	public class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, UserProfile>
	{
		private readonly ApplicationDbContext dbContext;
		private readonly ICurrentUserService currentUserService;

		public GetUserProfileHandler(ApplicationDbContext dbContext, ICurrentUserService currentUserService)
		{
			this.dbContext = dbContext;
			this.currentUserService = currentUserService;
		}

		public async Task<UserProfile> Handle(GetUserProfileQuery query, CancellationToken cancellationToken)
		{
			var profile = await dbContext.Profiles
				.Include(x => x.Addresses)
				.FirstOrDefaultAsync(x => x.Id == currentUserService.UserId, cancellationToken);

			return profile;
		}
	}
}
