using AutoMapper;
using MiniBlog.ViewModels;

namespace MiniBlog.AutoMapper
{
    public class ImageMappingProfile : Profile
    {
        public ImageMappingProfile()
        {
            CreateMap<Core.Entities.Image, Image>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => src.ImageName))
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath));
        }
    }
}
