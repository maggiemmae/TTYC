using MediatR;
using TTYC.Domain;
using TTYC.Persistence;

namespace TTYC.Application.Users.Commands.AddProfile
{
	public class AddProfileHandler : IRequestHandler<AddProfileCommand>
	{
		private readonly ApplicationDbContext dbContext;

		public AddProfileHandler(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<Unit> Handle(AddProfileCommand command, CancellationToken cancellationToken)
		{
			var user = dbContext.Users.FirstOrDefault(x => x.PhoneNumber == command.PhoneNumber);

			var profile = new UserProfile
			{
				UserId = user.UserId,
				Name = command.Name,
				Surname = command.Surname,
				Email = command.Email,

				Addresses = new List<Address>
				{
					new Address
					{
						AddressId = Guid.NewGuid(),
						Street = command.Street,
						HouseNumber = command.HouseNumber,
						FlatNumber = command.FlatNumber,
						Floor = command.Floor
					}
				}
			};

			await dbContext.Profiles.AddAsync(profile, cancellationToken);
			await dbContext.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
