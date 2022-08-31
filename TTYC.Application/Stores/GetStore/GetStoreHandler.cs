using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Stores.GetStore
{
    public class GetStoreHandler : IRequestHandler<GetStoreQuery, Store>
    {
        private readonly ApplicationDbContext dbContext;

        public GetStoreHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Store> Handle(GetStoreQuery query, CancellationToken cancellationToken)
        {
            var store = await dbContext.Stores
                .Include(x => x.Products.Where(x => x.IsActive == true))
                .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
            return store;
        }
    }
}
