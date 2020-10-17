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

        [HttpGet("CreateBlogPost")]
        public async Task<IActionResult> CreateBlogPost(BlogPost blogPost)
        {
            //var user = await userManager.GetUserAsync(User);
            //var isAdmin = await userManager.IsInRoleAsync(user, Roles.Admin);
            //var userAgeCategory = user.GetAgeRestrictionCategory();

            //_logger.LogInformation("User:{0} requested blog post:{1}", user.UserName, id);
            //var post = await blogPostService.GetBlogPost(id);
            //if ((post.AllowedAge & userAgeCategory) != userAgeCategory && !isAdmin)
            //{
            //    ModelState.AddModelError("", "You do not match any of the age categories to see this content.");
            //    return BadRequest(ModelState);
            //}
            return Ok();
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
                ModelState.AddModelError("","You do not match any of the age categories to see this content.");
                return BadRequest(ModelState);
            }
            return Ok(mapper.Map<BlogPost>(post));
        }

        [HttpGet("GetAllBlogPost")]
        public async Task<IActionResult> GetAllBlogPost(int page = 0)
        {
            var user = await userManager.GetUserAsync(User);
            _logger.LogInformation("User:{0} requested blog posts page:{1}", user.UserName,page);
            var isAdmin = await userManager.IsInRoleAsync(user, Roles.Admin);
            var posts = await blogPostService.GetBlogPostsForUser(user,isAdmin);
            return Ok(mapper.Map<IEnumerable<BlogPostBase>, IEnumerable<BlogPost>>(posts));
        }

        [HttpGet("GetLastestPosts")]
        public async Task<IActionResult> GetLastestPosts()
        {
            var user = await userManager.GetUserAsync(User);
            var isAdmin = await userManager.IsInRoleAsync(user, Roles.Admin);
            var posts = await blogPostService.GetBlogPostsForUser(user,isAdmin);
            return Ok(mapper.Map<IEnumerable<BlogPostBase>, IEnumerable<BlogPost>>(posts));
        }
    }
}
