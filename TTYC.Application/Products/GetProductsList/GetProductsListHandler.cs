using MediatR;
using TTYC.Application.Models;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Products.GetProductsList
{
    public class GetProductsListHandler : IRequestHandler<GetProductsListQuery, PagedList<Product>>
    {
        private readonly ApplicationDbContext dbContext;

        public GetProductsListHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PagedList<Product>> Handle(GetProductsListQuery query, CancellationToken cancellationToken)
        {
            var products = await PagedList<Product>.ToPagedListAsync(
                dbContext.Products.Where(x => x.IsActive),
                query.PageNumber,
                query.PageSize);

            return products;
        }
    }
}
