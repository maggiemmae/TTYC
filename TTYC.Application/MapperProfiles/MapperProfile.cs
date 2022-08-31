using AutoMapper;
using Stripe.Checkout;
using TTYC.Application.Adresses.AddAddress;
using TTYC.Application.Adresses.EditAddress;
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
            CreateMap<AddProfileCommand, UserProfile>().ForMember(x => x.Addresses, opt => opt.MapFrom<AddressResolver>());
            CreateMap<Models.Address, Address>().ForMember(x => x.Id, opt => Guid.NewGuid());
            CreateMap<Product, ProductInfrastructure>();
            CreateMap<AddAddressCommand, Address>().ForMember(x => x.Id, opt => Guid.NewGuid());
            CreateMap<CartItem, SessionLineItemOptions>().ForMember(x => x.Price, opt => opt.MapFrom(x => x.PriceId))
                .ForMember(x => x.Quantity, opt => opt.MapFrom(x => x.Count));
        }

        private class AddressResolver : IValueResolver<AddProfileCommand, UserProfile, IList<Address>>
        {
            private readonly IMapper mapper;

            public AddressResolver(IMapper mapper)
            {
                this.mapper = mapper;
            }

            public IList<Address> Resolve(AddProfileCommand source, UserProfile destination, IList<Address> destMember, ResolutionContext context)
            {
                return mapper.Map<IList<Address>>(new List<Models.Address> { source.Address });
            }
        }
    }
}
