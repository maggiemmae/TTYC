using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Persistence;

namespace TTYC.Application.Adresses.DeleteAddress
{
    public class DeleteAddressHandler : IRequestHandler<DeleteAddressCommand, Unit>
    {
        private readonly ApplicationDbContext dbContext;

        public DeleteAddressHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteAddressCommand command, CancellationToken cancellationToken)
        {
            var address = await dbContext.Addresses.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
            dbContext.Addresses.Remove(address);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
