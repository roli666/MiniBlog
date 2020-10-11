using Microsoft.EntityFrameworkCore;
using MiniBlog.Core.Entities;
using MiniBlog.Core.Enums;
using MiniBlog.Core.Interfaces.Repositories;
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

        public async Task<IEnumerable<BlogPostBase>> GetAllBlogPosts()
        {
            return await db.BlogPosts.ToListAsync();
        }

        public async Task<IEnumerable<BlogPostBase>> GetAllBlogPostsByAgeRestriction(AgeRestrictionCategories allowedAge)
        {
            return await db.BlogPosts.Where(bp => (bp.AllowedAge & allowedAge) == allowedAge).ToListAsync();
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

        public async Task<T> UpdateBlogPost<T>(T blogPost) where T : BlogPostBase
        {
            var entity = db.BlogPosts.Attach(blogPost);
            entity.State = EntityState.Modified;
            return await Task.FromResult(blogPost);
        }
    }
}