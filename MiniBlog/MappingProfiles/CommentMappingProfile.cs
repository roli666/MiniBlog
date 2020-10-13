using AutoMapper;
using MiniBlog.ViewModels;

namespace MiniBlog.MappingProfiles
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<Core.Entities.Comment, Comment>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OwnerPostId, opt => opt.MapFrom(src => src.OwnerPostId))
                .ForMember(dest => dest.OwnerUser, opt => opt.MapFrom(src => src.OwnerUser.UserName))
                .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.Parent));
        }
    }
}
