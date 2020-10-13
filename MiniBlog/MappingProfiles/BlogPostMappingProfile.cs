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
            CreateMap<BlogPostBase,BlogPost>().ConstructUsing(bb => new BlogPost { 
                AllowedAges = bb.AllowedAge.GetAgeRestrictions(),
                BackgroundImage = bb.BackgroundImage.ImagePath.ToString(),
                Category = bb.Category,
                Content = bb.Content,
                CreatedBy = bb.CreatedBy.UserName,
                CreatedOn = bb.CreatedOn,
                Id = bb.Id,
                Title = bb.Title
            });
        }
    }
}
