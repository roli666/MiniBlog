using AutoMapper;
using MiniBlog.Core.Entities;
using MiniBlog.Core.Helpers;
using MiniBlog.ViewModels;

namespace MiniBlog.AutoMapper
{
    public class BlogPostMappingProfile : Profile
    {
        public BlogPostMappingProfile()
        {
            CreateMap<BlogPostBase, BlogPost>()
                .ForMember(dest => dest.AllowedAges, opt => opt.MapFrom(src => src.AllowedAge.GetAgeRestrictions()))
                .ForMember(dest => dest.BackgroundImage, opt => opt.MapFrom(src => src.BackgroundImage.ImagePath))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.UserName))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.GetCommentCount(src.Comments)));
        }
    }
}
