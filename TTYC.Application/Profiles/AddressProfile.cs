using AutoMapper;
using TTYC.Application.Models;
using TTYC.Domain;

namespace TTYC.Application.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<AddressDTO, Address>();
        }
    }
}
