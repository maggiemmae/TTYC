using AutoMapper;
using MediatR;
using TTYC.Application.Interfaces;
using TTYC.Domain;
using TTYC.Persistence;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TTYC.Application.Adresses.AddAddress
{
    public class AddAddressHandler : IRequestHandler<AddAddressCommand, Guid>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;
        private readonly IMapper mapper;

        public AddAddressHandler(ApplicationDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(AddAddressCommand command, CancellationToken cancellationToken)
        {
            var address = mapper.Map<Address>(command);
            address.UserId = currentUserService.UserId;
            address.Id = Guid.NewGuid();

            dbContext.Addresses.Add(address);
            await dbContext.SaveChangesAsync(cancellationToken);

            return address.Id;
        }
    }
}
