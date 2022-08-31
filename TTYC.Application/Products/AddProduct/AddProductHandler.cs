using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stripe;
using TTYC.Domain;
using TTYC.Persistence;
using Product = TTYC.Domain.Product;

namespace TTYC.Application.Products.AddProduct
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, Guid>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public AddProductHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            var options = new ProductCreateOptions
            {
                Name = command.Name,
                Description = command.Description,
                DefaultPriceData = new ProductDefaultPriceDataOptions
                {
                    Currency = "usd",
                    UnitAmountDecimal = command.Price * 100
                }
            };
            var service = new ProductService();
            var item = service.Create(options);

            var stores = await dbContext.Stores
                .Where(x => command.StoreIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            var product = mapper.Map<Product>(command);
            product.PriceId = item.DefaultPriceId;
            product.Stores = stores;

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);
            
            return product.Id;
        }
    }
}
