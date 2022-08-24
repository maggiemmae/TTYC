using MediatR;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Users.AddUser
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, Guid>
    {
        private readonly ApplicationDbContext dbContext;

        public AddUserHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guid> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                PhoneNumber = command.PhoneNumber,
                Password = PasswordHelper.HashPassword(command.Password),
                Role = command.Role
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
