using AutoMapper;
using TTYC.Application.Models;
using TTYC.Application.Products.AddProduct;
using TTYC.Application.Stores.AddStore;
using TTYC.Application.Users.AddProfile;
using TTYC.Constants;
using TTYC.Domain;
using Address = TTYC.Domain.Address;

namespace TTYC.Application.MapperProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserInfrastructure>().ForMember(x => x.Status, opt => opt.MapFrom(x => x.LockoutEnd < DateTime.UtcNow ? Status.Active : Status.Blocked));
            CreateMap<AddStoreCommand, Store>().ForMember(x => x.Id, opt => Guid.NewGuid());
            CreateMap<AddProductCommand, Product>().ForMember(x => x.Id, opt => Guid.NewGuid());
            CreateMap<AddProfileCommand, UserProfile>();
            CreateMap<Models.Address, Address>().ForMember(x => x.Id, opt => Guid.NewGuid());
        }
    }
}
