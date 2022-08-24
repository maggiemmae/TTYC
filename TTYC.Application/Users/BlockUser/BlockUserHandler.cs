using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Persistence;

namespace TTYC.Application.Users.BlockUser
{
    public class BlockUserHandler : IRequestHandler<BlockUserCommand, DateTime>
    {
        private readonly ApplicationDbContext dbContext;

        public BlockUserHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<DateTime> Handle(BlockUserCommand command, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
            user.LockoutEnd = DateTime.UtcNow.AddDays(command.Days).AddHours(command.Hours);
            
            dbContext.Update(user);
            await dbContext.SaveChangesAsync(cancellationToken);
            return user.LockoutEnd;
        }
    }
}
