using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Persistence;

namespace TTYC.Application.Adresses.EditAddress
{
    public class EditAddressHandler : IRequestHandler<EditAddressCommand, Unit>
    {
        private readonly ApplicationDbContext dbContext;

        public EditAddressHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(EditAddressCommand command, CancellationToken cancellationToken)
        {
            var address = await dbContext.Addresses.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            address.Street = command.Street;
            address.HouseNumber = command.HouseNumber;
            address.FlatNumber = command.FlatNumber;
            address.Floor = command.Floor;
            address.IsDefault = command.IsDefault;
            address.LastUpdated = command.LastUpdated;
            address.Latitude = command.Latitude;
            address.Longitude = command.Longitude;

            dbContext.Update(address);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
