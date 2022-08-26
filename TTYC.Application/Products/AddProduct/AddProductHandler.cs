using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Domain;
using TTYC.Persistence;

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
            var stores = await dbContext.Stores
                .Where(x => command.StoreIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            var product = mapper.Map<Product>(command);
            product.Stores = stores;

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
