using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace MokshenAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
