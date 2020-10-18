using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniBlog.Core.Constants;
using MiniBlog.Core.Entities;
using MiniBlog.Core.Helpers;
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

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("CreateBlogPost")]
        public async Task<IActionResult> CreateBlogPost(BlogPost blogPost)
        {
            var user = await userManager.GetUserAsync(User);
            _logger.LogInformation("User:{0} requested creation of blogpost:{1}", user.UserName, blogPost);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BlogPostBase createdBlogPost = null;
            switch (blogPost.Category)
            {
                case SportBlogPost.Category:
                    createdBlogPost = await blogPostService.CreateBlogPost(new SportBlogPost());
                    break;
                case ChildBlogPost.Category:
                    createdBlogPost = await blogPostService.CreateBlogPost(new ChildBlogPost());
                    break;
                case PublicLifeBlogPost.Category:
                    createdBlogPost = await blogPostService.CreateBlogPost(new PublicLifeBlogPost());
                    break;
                default:
                    BadRequest("Selected category does not exist.");
                    break;
            }
            return Ok(createdBlogPost);
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await blogPostService.GetCategories());
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("Delete/{id:guid}")]
        public async Task<IActionResult> DeleteBlogPost(Guid id)
        {
            var ret = await blogPostService.DeleteBlogPost(id);
            return Ok(ret);
        }

        [HttpGet("Post/{id:guid}")]
        public async Task<IActionResult> GetBlogPost(Guid id)
        {
            var user = await userManager.GetUserAsync(User);
            var isAdmin = await userManager.IsInRoleAsync(user, Roles.Admin);
            var userAgeCategory = user.GetAgeRestrictionCategory();

            _logger.LogInformation("User:{0} requested blog post:{1}", user.UserName, id);
            var post = await blogPostService.GetBlogPost(id);
            if ((post.AllowedAge & userAgeCategory) != userAgeCategory && !isAdmin)
            {
                ModelState.AddModelError("", "You do not match any of the age categories to see this content.");
                return BadRequest(ModelState);
            }
            return Ok(mapper.Map<BlogPost>(post));
        }

        [HttpGet("GetAllBlogPost/{filter}")]
        public async Task<IActionResult> GetAllBlogPost(BlogPostFilter filter)
        {
            var user = await userManager.GetUserAsync(User);
            _logger.LogInformation("User:{0} requested blog posts by filter:{1}", user.UserName, filter);
            var isAdmin = await userManager.IsInRoleAsync(user, Roles.Admin);
            var posts = await blogPostService.GetBlogPostsForUser(user, isAdmin, filter);
            return Ok(mapper.Map<IEnumerable<BlogPostBase>, IEnumerable<BlogPost>>(posts));
        }

        [HttpGet("GetAllBlogPost")]
        public async Task<IActionResult> GetAllBlogPost()
        {
            var user = await userManager.GetUserAsync(User);
            _logger.LogInformation("User:{0} requested blog posts", user.UserName);
            var isAdmin = await userManager.IsInRoleAsync(user, Roles.Admin);
            var posts = await blogPostService.GetBlogPostsForUser(user, isAdmin, null);
            return Ok(mapper.Map<IEnumerable<BlogPostBase>, IEnumerable<BlogPost>>(posts));
        }

        [HttpGet("GetLastestPosts")]
        public async Task<IActionResult> GetLastestPosts()
        {
            var user = await userManager.GetUserAsync(User);
            var isAdmin = await userManager.IsInRoleAsync(user, Roles.Admin);
            var posts = await blogPostService.GetLatestBlogPostsForUser(user, isAdmin, 3);
            return Ok(mapper.Map<IEnumerable<BlogPostBase>, IEnumerable<BlogPost>>(posts));
        }
    }
}
