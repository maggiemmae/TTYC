using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Persistence;

namespace TTYC.Application.Stores.EditStore
{
    public class EditStoreHandler : IRequestHandler<EditStoreCommand, Unit>
    {
        private readonly ApplicationDbContext dbContext;

        public EditStoreHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(EditStoreCommand command, CancellationToken cancellationToken)
        {
            var store = await dbContext.Stores.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            store.Name = command.Name;
            store.Longitude = command.Longitude;
            store.Latitude = command.Latitude;

            dbContext.Update(store);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
