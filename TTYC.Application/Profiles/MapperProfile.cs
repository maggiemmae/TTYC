using AutoMapper;
using TTYC.Application.Models;
using TTYC.Application.Stores.AddStore;
using TTYC.Domain;

namespace TTYC.Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserInfrastructure>().ForMember(x => x.Status, opt => opt.MapFrom(x => x.LockoutEnd < DateTime.UtcNow ? "Active" : "Blocked"));
            CreateMap<AddStoreCommand, Store>().ForMember(x => x.Id, opt => Guid.NewGuid());
        }
    }
}
