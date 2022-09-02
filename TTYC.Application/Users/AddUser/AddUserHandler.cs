using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var userExists = await dbContext.Users
                .FirstOrDefaultAsync(x => x.PhoneNumber == command.PhoneNumber, cancellationToken);
            if (userExists != null)
            {
                throw new Exception("User with such phone number is already exist");
            };

            var user = new User
            {
                Id = Guid.NewGuid(),
                PhoneNumber = command.PhoneNumber,
                Password = PasswordHelper.HashPassword(command.Password),
                Role = command.Role,
                IsPasswordReseted = false
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
