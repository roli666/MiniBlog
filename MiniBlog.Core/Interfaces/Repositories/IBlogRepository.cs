using MiniBlog.Core.Entities;
using MiniBlog.Core.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Core.Interfaces.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogPostBase>> GetAllBlogPosts();

        Task<BlogPostBase> GetBlogPostById(Guid blogPostId);

        Task<IEnumerable<T>> GetAllBlogPostsByCategory<T>() where T : BlogPostBase;

        Task<IEnumerable<BlogPostBase>> GetAllBlogPostsByAgeRestriction(AgeRestrictionCategories allowedAges);

        Task<T> CreateBlogPost<T>(T blogPost) where T : BlogPostBase;

        Task<T> UpdateBlogPost<T>(T blogPost) where T : BlogPostBase;

        Task<T> DeleteBlogPost<T>(T blogPost) where T : BlogPostBase;

        Task<int> Commit();
    }
}