using AutoMapper;
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
			var profile = new UserProfile
			{
				Id = user.Id,
				Name = command.Name,
				Surname = command.Surname,
				Email = command.Email,
				Addresses = new List<Address>
				{
					mapper.Map<Address>(command.Address)
				}
			};

			await dbContext.Profiles.AddAsync(profile, cancellationToken);
			await dbContext.SaveChangesAsync(cancellationToken);

			return profile.Id;
		}
	}
}
