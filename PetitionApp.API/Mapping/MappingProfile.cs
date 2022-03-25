using AutoMapper;
using PetitionApp.API.DTO;
using PetitionApp.Core.Models;

namespace PetitionApp.API.Mapping
{
    public class MappingProfile : Profile
    {
       public MappingProfile()
        {

            CreateMap<CreatePetitionDTO, Petition>()
                .ForMember("CreationDate", opt => opt.MapFrom(_ => DateOnly.FromDateTime(DateTime.Now)))
                .ForMember("Author", opt => opt.MapFrom(c => new User()));

            CreateMap<RegisterDTO, User>();

            CreateMap<Petition, PetitionDTO>()
                .ForMember("CreatedDate", opt => opt.MapFrom(c => DateTime.Parse(c.CreationDate.ToString())))
                .ForMember("Tags", opt => opt.MapFrom(c => c.PetitionTags.Select(t => new Tag() { Id = t.TagId, Name = t.Tag.Name })))
                .ForMember("Author", opt => opt.MapFrom(c => new UserDTO() { Id = c.Author.Id, UserName = c.Author.UserName }));
        }
    }
}
