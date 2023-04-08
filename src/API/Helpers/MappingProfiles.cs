using API.Dtos.Game;
using API.Dtos.User;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Game, GameDto>()
            .ForMember(dest => dest.Mode, opt => opt.MapFrom(src => src.Mode.ToString()))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category!.Name))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher!.Name));



        CreateMap<ApplicationUser, UserListDto>();
        CreateMap<ApplicationUser, UserDetailsDto>();
    }
}
