using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Application.Interfaces;
using TTYC.Persistence;

namespace TTYC.Application.Carts.DeleteProductFromCart
{
    public class DeleteProductFromCartHandler : IRequestHandler<DeleteProductFromCartCommand, Unit>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public DeleteProductFromCartHandler(ApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteProductFromCartCommand command, CancellationToken cancellationToken)
        {
            var cartItem = await dbContext.CartItems.FirstOrDefaultAsync(
                x => x.CartId == currentUserService.UserId 
                && x.ProductId == command.ProductId, cancellationToken);

            dbContext.CartItems.Remove(cartItem);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
