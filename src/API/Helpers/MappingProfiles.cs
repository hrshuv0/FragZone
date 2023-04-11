using API.Dtos.Game;
using API.Dtos.User;
using AutoMapper;
using Core.Dtos.Identity;
using Core.Dtos.PhotosDto;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.Photos;
using Infrastructure.Utility;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Game, GameDto>()
            .ForMember(dest => dest.Mode, opt => opt.MapFrom(src => src.Mode.ToString()))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category!.Name))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher!.Name));



        CreateMap<ApplicationUser, UserListDto>()
            .ForMember(dest => dest.PhotoUrl,
                opt => opt.MapFrom(src => src.Photos!.FirstOrDefault(x => x.IsMain)!.Url))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
        
        CreateMap<ApplicationUser, UserDetailsDto>()
            .ForMember(dest => dest.PhotoUrl,
                opt => opt.MapFrom(src => src.Photos!.FirstOrDefault(x => x.IsMain)!.Url))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
        
        CreateMap<ApplicationUser, UserDetailsForReturnDto>()
            .ForMember(dest => dest.PhotoUrl,
                opt => opt.MapFrom(src => src.Photos!.FirstOrDefault(x => x.IsMain)!.Url))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

        
        
        CreateMap<Photo, PhotoDetailsDto>();
        CreateMap<Photo, PhotoReturnDto>();
        CreateMap<PhotoCreationDto, Photo>();
    }
}
