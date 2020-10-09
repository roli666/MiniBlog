using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniBlog.Core.Entities;
using MiniBlog.Core.Interfaces.Repositories;

namespace MiniBlog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentRepository repo;

        public CommentController(ILogger<CommentController> logger, ICommentRepository repo)
        {
            _logger = logger;
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<Comment>> GetComments(Guid id)
        {
            return await repo.GetCommentsRelatedToBlogPost(id);
        }
    }
}
