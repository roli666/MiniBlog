using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniBlog.Core.Constants;
using MiniBlog.Core.Entities;
using MiniBlog.Core.Interfaces;
using MiniBlog.Core.Models;

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

        public BlogPostController(ILogger<BlogPostController> logger, IBlogPostService blogPostService, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this.blogPostService = blogPostService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IEnumerable<BlogPostBase>> Get()
        {
            var user = await userManager.GetUserAsync(User);
            return await blogPostService.GetBlogPostsForUser(user);
        }
    }
}
