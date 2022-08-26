using AutoMapper;
using MediatR;
using TTYC.Application.Interfaces;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Users.AddProfile
{
    public class AddProfileHandler : IRequestHandler<AddProfileCommand, Guid>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;
        private readonly IMapper mapper;

        public AddProfileHandler(ApplicationDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(AddProfileCommand command, CancellationToken cancellationToken)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Id == currentUserService.UserId);

            var profile = mapper.Map<UserProfile>(command);
            profile.Id = user.Id;

            dbContext.Profiles.Add(profile);
            await dbContext.SaveChangesAsync(cancellationToken);

            return profile.Id;
        }
    }
}
