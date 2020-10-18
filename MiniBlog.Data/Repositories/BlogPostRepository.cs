using Microsoft.EntityFrameworkCore;
using MiniBlog.Core.Entities;
using MiniBlog.Core.Enums;
using MiniBlog.Core.Interfaces.Repositories;
using MiniBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.Data.Repositories
{
    public class BlogPostRepositoryRepository : IBlogRepository
    {
        private readonly MiniBlogDBContext db;

        public BlogPostRepositoryRepository(MiniBlogDBContext db)
        {
            this.db = db;
        }

        public async Task<int> Commit()
        {
            return await db.SaveChangesAsync();
        }

        public async Task<T> CreateBlogPost<T>(T blogPost) where T : BlogPostBase
        {
            await db.AddAsync(blogPost);
            return blogPost;
        }

        public async Task<T> DeleteBlogPost<T>(T blogPost) where T : BlogPostBase
        {
            db.Remove(blogPost);
            return await Task.FromResult(blogPost);
        }

        private IQueryable<BlogPostBase> ApplyFilters(DbSet<BlogPostBase> blogPosts, BlogPostFilter filter)
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
            return await ApplyFilters(db.BlogPosts, filter).ToListAsync();
        }

        public async Task<IEnumerable<BlogPostBase>> GetAllBlogPostsByAgeRestriction(AgeRestrictionCategories allowedAge, BlogPostFilter filter = null)
        {
            return await ApplyFilters(db.BlogPosts, filter).Where(bp => (bp.AllowedAge & allowedAge) == allowedAge).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllBlogPostsByCategory<T>() where T : BlogPostBase
        {
            DbSet<T> dbset = db.Set<T>();
            return await dbset.ToListAsync();
        }

        public async Task<BlogPostBase> GetBlogPostById(Guid blogPostId)
        {
            return await db.BlogPosts.FirstOrDefaultAsync(b => b.Id == blogPostId);
        }

        public async Task<IEnumerable<BlogPostBase>> GetLatestBlogPosts(int limit)
        {
            return await db.BlogPosts
                .OrderBy(bp => bp.CreatedOn)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogPostBase>> GetLatestBlogPosts(AgeRestrictionCategories allowedAges, int limit)
        {
            return await db.BlogPosts
                .Where(bp => (bp.AllowedAge & allowedAges) == allowedAges)
                .OrderBy(bp => bp.CreatedOn)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<T> UpdateBlogPost<T>(T blogPost) where T : BlogPostBase
        {
            var entity = db.BlogPosts.Attach(blogPost);
            entity.State = EntityState.Modified;
            return await Task.FromResult(blogPost);
        }
    }
}