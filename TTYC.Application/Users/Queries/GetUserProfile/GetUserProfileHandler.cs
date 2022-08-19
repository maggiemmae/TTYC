using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Users.Queries.GetUserProfile
{
	public class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, UserProfile>
	{
		private readonly ApplicationDbContext dbContext;

		public GetUserProfileHandler(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<UserProfile> Handle(GetUserProfileQuery query, CancellationToken cancellationToken)
		{
			var user = dbContext.Users.FirstOrDefault(x => x.PhoneNumber == query.PhoneNumber);
			var profile = await dbContext.Profiles.Include(x => x.Addresses).FirstOrDefaultAsync(x => x.UserId == user.UserId, cancellationToken);

			return profile;
		}
	}
}
