using AutoMapper;
using PetitionApp.API.Models;
using PetitionApp.Core.Models;

namespace PetitionApp.API.Mapping
{
    public class MappingProfile : Profile
    {
       public MappingProfile()
        {
            CreateMap<RegisterModel, User>();
        }
    }
}
