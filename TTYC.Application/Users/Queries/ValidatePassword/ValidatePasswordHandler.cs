using MediatR;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Users.Queries.ValidatePassword
{
    public class ValidatePasswordHandler : IRequestHandler<ValidatePasswordQuery, User>
    {
        private readonly ApplicationDbContext dbContext;

        public ValidatePasswordHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> Handle(ValidatePasswordQuery query, CancellationToken cancellationToken)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.PhoneNumber == query.UserName);
            return user;
        }
    }
}
