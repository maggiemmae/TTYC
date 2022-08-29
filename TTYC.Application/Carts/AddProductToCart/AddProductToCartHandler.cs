using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Application.Interfaces;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Carts.AddProductToCart
{
    public class AddProductToCartHandler : IRequestHandler<AddProductToCartCommand, Unit>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public AddProductToCartHandler(ApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(AddProductToCartCommand command, CancellationToken cancellationToken)
        {
            var cartItem = await dbContext.CartItems.FirstOrDefaultAsync(
                x => x.CartId == currentUserService.UserId 
                && x.ProductId == command.ProductId, cancellationToken);

            var product = await dbContext.Products
                .FirstOrDefaultAsync(x => x.Id == command.ProductId, cancellationToken);

            if(cartItem == null)
            {
                cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = currentUserService.UserId,
                    ProductId = command.ProductId,
                    Count = command.Count,
                    PriceId = product.PriceId
                };
                dbContext.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Count += command.Count;
            }

            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
