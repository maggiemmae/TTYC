using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Users.Queries.GetUsers
{
	public class GetUserHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
	{
		private readonly ApplicationDbContext dbContext;

		public GetUserHandler(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
		{
			var users = await dbContext.Users.ToListAsync();
			return users;
		}
	}
}
