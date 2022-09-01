using MediatR;
using Microsoft.EntityFrameworkCore;
using Stripe;
using TTYC.Persistence;

namespace TTYC.Application.Products.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly ApplicationDbContext dbContext;

        public DeleteProductHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            var options = new ProductUpdateOptions
            {
                Active = false
            };
            var service = new ProductService();
            service.Update(product.StripeId, options);

            product.IsActive = false;
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
