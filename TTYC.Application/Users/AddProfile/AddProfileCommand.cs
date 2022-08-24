using MediatR;
using TTYC.Application.Models;

namespace TTYC.Application.Users.AddProfile
{
    public class AddProfileCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public Address Address { get; set; }
    }
}
