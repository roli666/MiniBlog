using MiniBlog.Core.Entities;
using MiniBlog.Core.Enums;
using MiniBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Core.Interfaces.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogPostBase>> GetAllBlogPosts(BlogPostFilter filter);

        Task<BlogPostBase> GetBlogPostById(Guid blogPostId);

        Task<IEnumerable<T>> GetAllBlogPostsByCategory<T>() where T : BlogPostBase;

        Task<IEnumerable<BlogPostBase>> GetLatestBlogPosts(int limit);

        Task<IEnumerable<BlogPostBase>> GetLatestBlogPosts(AgeRestrictionCategories allowedAges, int limit);

        Task<IEnumerable<BlogPostBase>> GetAllBlogPostsByAgeRestriction(AgeRestrictionCategories allowedAges, BlogPostFilter filter);

        Task<T> CreateBlogPost<T>(T blogPost) where T : BlogPostBase;

        Task<T> UpdateBlogPost<T>(T blogPost) where T : BlogPostBase;

        Task<T> DeleteBlogPost<T>(T blogPost) where T : BlogPostBase;

        Task<int> Commit();
    }
}