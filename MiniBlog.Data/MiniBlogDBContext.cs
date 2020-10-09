using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MiniBlog.Core.Entities;
using MiniBlog.Core.Models;

namespace MiniBlog.Data
{
    public class MiniBlogDBContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public MiniBlogDBContext(DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<SportBlogPost> SportBlogPosts { get; set; }
        public DbSet<PublicLifeBlogPost> PublicLifeBlogPosts { get; set; }
        public DbSet<ChildBlogPost> ChildBlogPosts { get; set; }
        public DbSet<BlogPostBase> BlogPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}