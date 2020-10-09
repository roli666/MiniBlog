using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniBlog.Core.Entities;
using MiniBlog.Core.Interfaces;

namespace MiniBlog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;
        private readonly IFileService fileService;
        private readonly string[] permittedExtensions = { ".png", ".jpg", ".jpeg", ".gif" };
        public FileController(ILogger<FileController> logger, IFileService fileService)
        {
            _logger = logger;
            this.fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (file.ContentType.StartsWith("image") && (!string.IsNullOrEmpty(ext) || permittedExtensions.Contains(ext)))
            {
                await fileService.SaveImage(file.OpenReadStream());
            }
            else
            {
                ModelState.AddModelError("File", $"The request couldn't be processed (Error 1).");
                //TODO: log
                return BadRequest(ModelState);
            }
            return Ok();
        }
    }
}
