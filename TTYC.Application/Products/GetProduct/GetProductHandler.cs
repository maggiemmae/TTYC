using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Application.Models;
using TTYC.Persistence;

namespace TTYC.Application.Products.GetProduct
{
    public class GetProductHandler : IRequestHandler<GetProductQuery, ProductInfrastructure>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetProductHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ProductInfrastructure> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
            return mapper.Map<ProductInfrastructure>(product);
        }
    }
}
