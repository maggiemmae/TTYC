using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Products.GetProduct
{
    public class GetProductHandler : IRequestHandler<GetProductQuery, Product>
    {
        private readonly ApplicationDbContext dbContext;

        public GetProductHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Product> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
            return product;
        }
    }
}
