using AutoMapper;
using MediatR;
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
            var product = mapper.Map<Product>(command);

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
