using MiniBlog.Core.Entities;
using MiniBlog.Core.Enums;
using MiniBlog.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.Data.InMemory
{
    public class InMemoryBlogPostRepository : IBlogRepository
    {
        private readonly List<BlogPostBase> blogPosts;
        public static readonly string SportBlogPostId = "2c4d7dd5-6f9c-4ad7-bc19-990e15327111";
        public static readonly string ChildBlogPostId = "80c536ba-1342-4f20-882e-40ef77980a92";
        public static readonly string PublicLifeBlogPostId = "28f5a806-387f-4756-a3fa-4cf72a6c66dc";

        public InMemoryBlogPostRepository()
        {
            blogPosts = new List<BlogPostBase>
            {
                new SportBlogPost
                {
                    Content="Valami sportos vmi",
                    CreatedOn=DateTime.Now,
                    Id=Guid.Parse(SportBlogPostId),
                    Title="Sportos title"
                },
                new ChildBlogPost
                {
                    Content="Valami gyerekes vmi",
                    CreatedOn=DateTime.Now,
                    Id=Guid.Parse(ChildBlogPostId),
                    Title="gyerekes title"
                },
                new PublicLifeBlogPost
                {
                    Content="Valami public life cucc",
                    CreatedOn=DateTime.Now,
                    Id=Guid.Parse(PublicLifeBlogPostId),
                    Title="public life title"
                },
            };
        }

        public async Task<int> Commit()
        {
            return await Task.FromResult(1);
        }

        public async Task<IEnumerable<BlogPostBase>> GetAllBlogPosts()
        {
            return await Task.FromResult(blogPosts);
        }

        public async Task<IEnumerable<T>> GetAllBlogPostsByCategory<T>() where T : BlogPostBase
        {
            return (IEnumerable<T>)await Task.FromResult(blogPosts.Where(b => b is T));
        }

        public async Task<IEnumerable<BlogPostBase>> GetAllBlogPostsByAgeRestriction(AgeRestrictionCategories allowedAges)
        {
            return await Task.FromResult(blogPosts.Where(b => b.AllowedAge == allowedAges));
        }

        public async Task<T> CreateBlogPost<T>(T blogPost) where T : BlogPostBase
        {
            blogPosts.Add(blogPost);
            return await Task.FromResult(blogPost);
        }

        public async Task<T> UpdateBlogPost<T>(T blogPost) where T : BlogPostBase
        {
            blogPosts[blogPosts.FindIndex(ind => ind.Equals(blogPost))] = blogPost;
            return await Task.FromResult(blogPost);
        }

        public async Task<T> DeleteBlogPost<T>(T blogPost) where T : BlogPostBase
        {
            blogPosts.Remove(blogPost);
            return await Task.FromResult(blogPost);
        }

        public async Task<BlogPostBase> GetBlogPostById(Guid blogPostId)
        {
            return await Task.FromResult(blogPosts.FirstOrDefault(b => b.Id == blogPostId));
        }
    }
}