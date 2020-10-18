using MiniBlog.Core.Entities;
using MiniBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Core.Interfaces
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPostBase>> GetBlogPostsForUser(ApplicationUser user, bool isAdmin, BlogPostFilter filter);
        Task<IEnumerable<BlogPostBase>> GetLatestBlogPostsForUser(ApplicationUser user, bool isAdmin, int limit);
        Task<BlogPostBase> GetBlogPost(Guid id);
        Task<BlogPostBase> DeleteBlogPost(Guid id);
        Task<BlogPostBase> CreateBlogPost<T>(T blogPost) where T : BlogPostBase;
        Task<IEnumerable<string>> GetCategories();
    }
}