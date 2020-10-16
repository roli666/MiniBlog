using MiniBlog.Core.Entities;
using MiniBlog.Core.Interfaces;
using MiniBlog.Core.Interfaces.Repositories;
using MiniBlog.Core.Models;
using System;
using System.Threading.Tasks;

namespace MiniBlog.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository imageRepo;

        public ImageService(IImageRepository imageRepo)
        {
            this.imageRepo = imageRepo;
        }

        public async Task<Image> GetImageById(Guid imageId)
        {
            return await imageRepo.GetImageById(imageId);
        }

        public async Task<Image> GetUserAvatar(ApplicationUser user)
        {
            return await imageRepo.GetAvatarImageForUser(user);
        }
    }
}
