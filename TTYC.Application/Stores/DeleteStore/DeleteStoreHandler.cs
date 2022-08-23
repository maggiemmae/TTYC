using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Persistence;

namespace TTYC.Application.Stores.DeleteStore
{
    public class DeleteStoreHandler : IRequestHandler<DeleteStoreCommand, Unit>
    {
        private readonly ApplicationDbContext dbContext;

        public DeleteStoreHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteStoreCommand command, CancellationToken cancellationToken)
        {
            var store = await dbContext.Stores.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
            dbContext.Stores.Remove(store);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
