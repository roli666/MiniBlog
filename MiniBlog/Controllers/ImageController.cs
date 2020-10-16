using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.Interfaces;
using MiniBlog.Core.Models;
using MiniBlog.ViewModels;

namespace MiniBlog.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public ImageController(
            IImageService imageService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this.imageService = imageService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<Image> Get()
        {
            //TODO: check if user is null
            var user = await userManager.GetUserAsync(User);
            return mapper.Map<Image>(await imageService.GetUserAvatar(user));
        }
    }
}
