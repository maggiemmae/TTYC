using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Products.EditProduct
{
    public class EditProductHandler : IRequestHandler<EditProductCommand, Unit>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public EditProductHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(EditProductCommand command, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;

            dbContext.Update(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
