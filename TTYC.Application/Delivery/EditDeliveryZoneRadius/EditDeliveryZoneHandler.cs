using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Persistence;

namespace TTYC.Application.Delivery.EditDeliveryZoneRadius
{
    public class EditDeliveryZoneHandler : IRequestHandler<EditDeliveryZoneCommand, Unit>
    {
        private readonly ApplicationDbContext dbContext;

        public EditDeliveryZoneHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(EditDeliveryZoneCommand command, CancellationToken cancellationToken)
        {
            var zone = await dbContext.DeliverySettings.FirstOrDefaultAsync(cancellationToken);

            zone.Radius = command.Radius;
            dbContext.Update(zone);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
