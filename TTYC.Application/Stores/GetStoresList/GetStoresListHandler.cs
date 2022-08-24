using MediatR;
using TTYC.Application.Models;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Stores.GetStoresList
{
    public class GetStoresListHandler : IRequestHandler<GetStoresListQuery, PagedList<Store>>
    {
        private readonly ApplicationDbContext dbContext;

        public GetStoresListHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PagedList<Store>> Handle(GetStoresListQuery query, CancellationToken cancellationToken)
        {
            var stores = await PagedList<Store>.ToPagedListAsync(
                dbContext.Stores,
                query.PageNumber,
                query.PageSize);

            return stores;
        }
    }
}
