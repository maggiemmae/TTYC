using MediatR;
using TTYC.Application.Interfaces;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Users.Commands.AddProfile
{
    public class AddProfileHandler : IRequestHandler<AddProfileCommand, Guid>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public AddProfileHandler(ApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(AddProfileCommand command, CancellationToken cancellationToken)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Id == currentUserService.UserId);

            var address = new List<Address>
                {
                    new Address
                    {
                        Id = Guid.NewGuid(),
                        Street = command.Address.Street,
                        HouseNumber = command.Address.HouseNumber,
                        FlatNumber = command.Address.HouseNumber,
                        Floor = command.Address.Floor
                    }
                };

            var profile = new UserProfile
            {
                Id = user.Id,
                Name = command.Name,
                Surname = command.Surname,
                Email = command.Email,
                Addresses = address
            };

            await dbContext.Profiles.AddAsync(profile, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return profile.Id;
        }
    }
}
