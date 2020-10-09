using MiniBlog.Core.Entities;
using MiniBlog.Core.Helpers;
using MiniBlog.Core.Interfaces;
using MiniBlog.Core.Interfaces.Repositories;
using MiniBlog.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Core.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogRepository blogPostRepository;

        public BlogPostService(IBlogRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public async Task<IEnumerable<BlogPostBase>> GetBlogPostsForUser(ApplicationUser user)
        {
            return await blogPostRepository.GetAllBlogPostsByAgeRestriction(user.GetAgeRestrictionCategories());
        }
    }
}