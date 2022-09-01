using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Application.Interfaces;
using TTYC.Application.Models;
using TTYC.Persistence;

namespace TTYC.Application.Orders.GetOrder
{
    public class GetOrderHandler : IRequestHandler<GetOrderQuery, OrderInfrastructure>
    {
        public readonly ApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;
        private readonly IMapper mapper;

        public GetOrderHandler(ApplicationDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
            this.mapper = mapper;
        }

        public async Task<OrderInfrastructure> Handle(GetOrderQuery query, CancellationToken cancellationToken)
        {
            var order = await dbContext.Orders
                .OrderBy(x => x.CreatedDate)
                .Include(x => x.CartItems)
                .Include(x => x.Address)
                .LastOrDefaultAsync(x => x.UserId == currentUserService.UserId, cancellationToken);
            return mapper.Map<OrderInfrastructure>(order);
        }
    }
}
