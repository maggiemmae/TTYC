using MediatR;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Users.Commands.AddUser
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

            await dbContext.Users.AddAsync(user, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
