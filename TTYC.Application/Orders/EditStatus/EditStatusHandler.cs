using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Persistence;

namespace TTYC.Application.Orders.EditStatus
{
    public class EditStatusHandler : IRequestHandler<EditStatusCommand, Unit>
    {
        private readonly ApplicationDbContext dbContext;

        public EditStatusHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(EditStatusCommand command, CancellationToken cancellationToken)
        {
            var order = await dbContext.Orders
                .OrderBy(x => x.CreatedDate)
                .Include(x => x.CartItems)
                .LastOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            order.Status = command.OrderStatus;

            dbContext.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
