using MediatR;
using Microsoft.EntityFrameworkCore;
using Stripe;
using TTYC.Constants;
using TTYC.Persistence;

namespace TTYC.Application.Products.EditProduct
{
    public class EditProductHandler : IRequestHandler<EditDeliveryZoneCommand, Unit>
    {
        private readonly ApplicationDbContext dbContext;

        public EditProductHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(EditDeliveryZoneCommand command, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            var priceOptions = new PriceCreateOptions
            {
                UnitAmountDecimal = command.Price * 100,
                Currency = PaymentOptions.Usd,
                Product = product.StripeId
            };
            var priceService = new PriceService();
            var price = priceService.Create(priceOptions);

            var productOptions = new ProductUpdateOptions
            {
                Name = command.Name,
                Description = command.Description,
                DefaultPrice = price.Id
            };
            var productService = new ProductService();
            productService.Update(product.StripeId, productOptions);

            var options = new PriceUpdateOptions
            {
                Active = false
            };
            var service = new PriceService();
            service.Update(product.PriceId, options);

            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.PriceId = price.Id;

            dbContext.Update(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
