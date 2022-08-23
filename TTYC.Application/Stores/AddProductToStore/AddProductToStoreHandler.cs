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
                .SingleAsync(p => p.Id == command.StoreId, cancellationToken);

            var product = dbContext.Products
                .Single(p => p.Id == command.ProductId);

            store.Products.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
