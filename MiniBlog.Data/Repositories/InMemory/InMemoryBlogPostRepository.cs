using MiniBlog.Core.Entities;
using MiniBlog.Core.Enums;
using MiniBlog.Core.Interfaces.Repositories;
using MiniBlog.Core.Models;
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
                    CreatedBy = new ApplicationUser{ UserName="Admin" },
                    Content="Valami sportos vmi",
                    CreatedOn=DateTime.Now,
                    Id=Guid.Parse(SportBlogPostId),
                    Title="Sportos title",
                    BackgroundImage=new Image
                    {
                        ImagePath=new Uri("img/InMemory/SportBlogPic.jpg",UriKind.Relative)
                    }
                },
                new ChildBlogPost
                {
                    CreatedBy = new ApplicationUser{ UserName="Admin" },
                    Content="Valami gyerekes vmi",
                    CreatedOn=DateTime.Now,
                    Id=Guid.Parse(ChildBlogPostId),
                    Title="gyerekes title",
                    BackgroundImage=new Image
                    {
                        ImagePath=new Uri("img/InMemory/ChildBlogPic.jpg",UriKind.Relative)
                    }
                },
                new PublicLifeBlogPost
                {
                    CreatedBy = new ApplicationUser{ UserName="Admin" },
                    Content="Valami public life cucc",
                    CreatedOn=DateTime.Now,
                    Id=Guid.Parse(PublicLifeBlogPostId),
                    Title="public life title",
                    BackgroundImage=new Image
                    {
                        ImagePath=new Uri("img/InMemory/PublicLifeBlogPic.jpg",UriKind.Relative)
                    }
                },
            };
        }

        public async Task<int> Commit()
        {
            return await Task.FromResult(1);
        }

        private IEnumerable<BlogPostBase> ApplyFilters(IEnumerable<BlogPostBase> blogPosts, BlogPostFilter filter)
        {
            var query = blogPosts.AsQueryable();
            if (filter != null)
            {
                query = query.Skip(filter.Page ?? 0 * filter.Limit ?? int.MaxValue).Take(filter.Limit ?? int.MaxValue);

                if (!string.IsNullOrEmpty(filter.ByCategory))
                    query = query.Where(bp => bp.Category == filter.ByCategory);

                if (!string.IsNullOrEmpty(filter.ByUser))
                    query = query.Where(bp => bp.CreatedBy.UserName == filter.ByUser);

                if (filter.InMonth.HasValue)
                    query = query.Where(bp =>
                    bp.CreatedOn.Year == filter.InMonth.Value.Year &&
                    bp.CreatedOn.Month == filter.InMonth.Value.Month
                    );
            }
            return query;
        }

        public async Task<IEnumerable<BlogPostBase>> GetAllBlogPosts(BlogPostFilter filter = null)
        {
            return await Task.FromResult(ApplyFilters(blogPosts, filter).ToList());
        }

        public async Task<IEnumerable<T>> GetAllBlogPostsByCategory<T>() where T : BlogPostBase
        {
            return (IEnumerable<T>)await Task.FromResult(blogPosts.Where(b => b is T));
        }

        public async Task<IEnumerable<BlogPostBase>> GetAllBlogPostsByAgeRestriction(AgeRestrictionCategories allowedAges, BlogPostFilter filter = null)
        {
            return await Task.FromResult(ApplyFilters(blogPosts, filter).Where(b => (b.AllowedAge & allowedAges) == allowedAges).ToList());
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

        public async Task<IEnumerable<BlogPostBase>> GetLatestBlogPosts(int limit)
        {
            return await Task.FromResult(blogPosts.OrderBy(bp => bp.CreatedOn).Take(limit));
        }

        public async Task<IEnumerable<BlogPostBase>> GetLatestBlogPosts(AgeRestrictionCategories allowedAges, int limit)
        {
            return await Task.FromResult(blogPosts.Where(b => (b.AllowedAge & allowedAges) == allowedAges).OrderBy(bp => bp.CreatedOn).Take(limit));
        }
    }
}