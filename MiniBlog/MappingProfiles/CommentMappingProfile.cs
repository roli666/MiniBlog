using AutoMapper;
using MiniBlog.ViewModels;

namespace MiniBlog.MappingProfiles
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<Core.Entities.Comment, Comment>().ConstructUsing(c => new Comment { 
                Id = c.Id,
                Content = c.Content,
                OwnerPostId = c.OwnerPostId,
                OwnerUser = c.OwnerUser.UserName
            });
        }
    }
}
