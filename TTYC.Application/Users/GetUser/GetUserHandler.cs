using MediatR;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Users.GetUser
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly ApplicationDbContext dbContext;

        public GetUserHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.PhoneNumber == query.UserName);
            return user;
        }
    }
}
