using MiniBlog.Core.Entities;
using MiniBlog.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Core.Interfaces
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPostBase>> GetBlogPostsForUser(ApplicationUser user,bool isAdmin);
    }
}