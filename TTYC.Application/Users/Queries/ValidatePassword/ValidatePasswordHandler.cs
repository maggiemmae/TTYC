using MediatR;
using TTYC.Persistence;

namespace TTYC.Application.Users.Queries.ValidatePassword
{
	public class ValidatePasswordHandler : IRequestHandler<ValidatePasswordQuery, bool>
	{
		private readonly ApplicationDbContext dbContext;

		public ValidatePasswordHandler(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public Task<bool> Handle(ValidatePasswordQuery query, CancellationToken cancellationToken)
		{
			var user = dbContext.Users.FirstOrDefault(x => x.PhoneNumber == query.UserName);

			var isMatch = PasswordHelper.VerifyHashedPassword(user.Password, query.Password);

			return Task.FromResult(isMatch);
		}
	}
}
