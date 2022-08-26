using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Application.Interfaces;
using TTYC.Application.Models;
using TTYC.Persistence;

namespace TTYC.Application.Carts.GetCart
{
    public class GetCartHandler : IRequestHandler<GetCartQuery, CartModel>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public GetCartHandler(ApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }

        public async Task<CartModel> Handle(GetCartQuery query, CancellationToken cancellationToken)
        {
            var cart = new CartModel
            {
                CartItems = await dbContext.CartItems
                    .Include(x => x.Product)
                    .Where(x => x.CartId == currentUserService.UserId)
                    .ToListAsync(cancellationToken),

                TotalSum = dbContext.CartItems
                    .Where(x => x.CartId == currentUserService.UserId)
                    .Select(x => x.Count * x.Product.Price).Sum()
            };
            return cart;
        }
    }
}
