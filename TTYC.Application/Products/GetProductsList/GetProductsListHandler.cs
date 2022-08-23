using MediatR;
using TTYC.Application.Models;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Products.GetProductsList
{
    public class GetProductsListHandler : IRequestHandler<GeStoresListQuery, PagedList<Product>>
    {
        private readonly ApplicationDbContext dbContext;

        public GetProductsListHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PagedList<Product>> Handle(GeStoresListQuery query, CancellationToken cancellationToken)
        {
            if(dbContext.Products == null)
            {
                throw new NullReferenceException("Products not found");
            }

            var products = await PagedList<Product>.ToPagedListAsync(
                dbContext.Products,
                query.PageNumber,
                query.PageSize);

            return products;
        }
    }
}
