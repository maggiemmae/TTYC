using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TTYC.Application.Models;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Users.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserInfrastructure>>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetUsersHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserInfrastructure>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var users = await dbContext.Users.ToListAsync(cancellationToken);
            var result = mapper.Map<IEnumerable<UserInfrastructure>>(users);
            return result;
        }
    }
}
