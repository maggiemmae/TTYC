using AutoMapper;
using MediatR;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Stores.AddStore
{
    public class AddStoreHandler : IRequestHandler<AddStoreCommand, Guid>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper; 

        public AddStoreHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(AddStoreCommand command, CancellationToken cancellationToken)
        {
            var store = mapper.Map<Store>(command);

            dbContext.Stores.Add(store);
            await dbContext.SaveChangesAsync(cancellationToken);

            return store.Id;
        }
    }
}
