using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniBlog.Core.Constants;
using MiniBlog.Core.Entities;
using MiniBlog.Core.Interfaces;
using MiniBlog.Core.Models;
using MiniBlog.ViewModels;

namespace MiniBlog.Controllers
{
    [Authorize(Policies.RequireMinimumRole)]
    [ApiController]
    [Route("[controller]")]
    public class BlogPostController : ControllerBase
    {
        private readonly ILogger<BlogPostController> _logger;
        private readonly IBlogPostService blogPostService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public BlogPostController(ILogger<BlogPostController> logger,
            IBlogPostService blogPostService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _logger = logger;
            this.blogPostService = blogPostService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpGet("GetBlogPosts")]
        public async Task<IEnumerable<BlogPost>> GetBlogPosts(int page = 0)
        {
            var user = await userManager.GetUserAsync(User);
            _logger.LogInformation("User:{0} requested blog posts page:{1}", user.UserName,page);
            var isAdmin = await userManager.IsInRoleAsync(user, Roles.Admin);
            var posts = await blogPostService.GetBlogPostsForUser(user,isAdmin);
            return mapper.Map<IEnumerable<BlogPostBase>, IEnumerable<BlogPost>>(posts);
        }

        [HttpGet("GetLatestBlogPosts")]
        public async Task<IEnumerable<BlogPost>> GetLastestPosts()
        {
            var user = await userManager.GetUserAsync(User);
            var isAdmin = await userManager.IsInRoleAsync(user, Roles.Admin);
            var posts = await blogPostService.GetBlogPostsForUser(user,isAdmin);
            return mapper.Map<IEnumerable<BlogPostBase>, IEnumerable<BlogPost>>(posts);
        }
    }
}
