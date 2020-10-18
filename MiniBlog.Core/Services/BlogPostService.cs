using MiniBlog.Core.Entities;
using MiniBlog.Core.Helpers;
using MiniBlog.Core.Interfaces;
using MiniBlog.Core.Interfaces.Repositories;
using MiniBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<BlogPostBase>> GetBlogPostsForUser(ApplicationUser user, bool isAdmin, BlogPostFilter filter)
        {
            if (isAdmin)
            {
                return await blogPostRepository.GetAllBlogPosts(filter);
            }
            return await blogPostRepository.GetAllBlogPostsByAgeRestriction(user.GetAgeRestrictionCategory(), filter);
        }
        public async Task<BlogPostBase> GetBlogPost(Guid id)
        {
            return await blogPostRepository.GetBlogPostById(id);
        }

        public async Task<IEnumerable<BlogPostBase>> GetLatestBlogPostsForUser(ApplicationUser user, bool isAdmin, int limit)
        {
            if (isAdmin)
            {
                return await blogPostRepository.GetLatestBlogPosts(limit);
            }
            return await blogPostRepository.GetLatestBlogPosts(user.GetAgeRestrictionCategory(), limit);
        }

        public async Task<BlogPostBase> DeleteBlogPost(Guid id)
        {
            var post = await blogPostRepository.GetBlogPostById(id);
            return await blogPostRepository.DeleteBlogPost(post);
        }

        public async Task<BlogPostBase> CreateBlogPost<T>(T blogPost) where T : BlogPostBase
        {
            return await blogPostRepository.CreateBlogPost(blogPost);
        }

        public async Task<IEnumerable<string>> GetCategories()
        {
            return await Task.FromResult(new List<string> {
                SportBlogPost.Category,
                PublicLifeBlogPost.Category,
                ChildBlogPost.Category
            });
        }
    }
}