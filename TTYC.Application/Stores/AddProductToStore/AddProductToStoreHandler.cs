using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Persistence;

namespace TTYC.Application.Stores.AddProductToStore
{
    public class AddProductToStoreHandler : IRequestHandler<AddProductToStoreCommand, Unit>
    {
        private readonly ApplicationDbContext dbContext;

        public AddProductToStoreHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddProductToStoreCommand command, CancellationToken cancellationToken)
        {
            var store = await dbContext.Stores
                .Include(p => p.Products)
                .FirstOrDefaultAsync(p => p.Id == command.StoreId, cancellationToken);

            var product = await dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

            store.Products.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
