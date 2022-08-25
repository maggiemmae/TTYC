using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Application.Interfaces;
using TTYC.Application.Models;
using TTYC.Persistence;

namespace TTYC.Application.Carts.GetCart
{
    public class GetCartHandler : IRequestHandler<GetCartQuery, ViewCart>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public GetCartHandler(ApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }

        public async Task<ViewCart> Handle(GetCartQuery query, CancellationToken cancellationToken)
        {
            var cart = new ViewCart
            {
                CartItems = await dbContext.CartItems
                .Include(x => x.Product)
                .Where(x => x.CartId == currentUserService.UserId)
                .ToListAsync(cancellationToken),


                TotalSum = (from items in dbContext.CartItems
                            where items.CartId == currentUserService.UserId
                            select items.Count * items.Product.Price).Sum()
            };
            return cart;
        }
    }
}
