using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniBlog.Core.Entities;
using MiniBlog.Core.Interfaces;
using MiniBlog.Core.Models;

namespace MiniBlog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;
        private readonly IFileService fileService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly string[] permittedExtensions = { ".png", ".jpg", ".jpeg", ".gif" };
        public FileController(ILogger<FileController> logger,
            IFileService fileService,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this.fileService = fileService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            var user = await userManager.GetUserAsync(User);
            if (file.ContentType.StartsWith("image") && (!string.IsNullOrEmpty(ext) || permittedExtensions.Contains(ext)))
            {
                await fileService.SaveImage(file.OpenReadStream());
            }
            else
            {
                _logger.LogInformation("User:{0} tried to upload a disallowed file:{1}", user.UserName, file.FileName);
                ModelState.AddModelError("File", $"The request couldn't be processed (Error 1).");
                return BadRequest(ModelState);
            }
            return Ok();
        }
    }
}
