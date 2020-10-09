using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MiniBlog.Core.Entities;
using MiniBlog.Core.Interfaces.Repositories;
using MiniBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly MiniBlogDBContext db;
        private readonly IWebHostEnvironment env;

        public ImageRepository(MiniBlogDBContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public async Task<Image> AddImage(Image image)
        {
            await db.Images.AddAsync(image);
            return image;
        }

        public Task<Image> GetAvatarImageForUser(ApplicationUser user)
        {
            return Task.FromResult(user.ProfilePicture ??
                new Image
                {
                    ImageName = "Placeholder",
                    ImagePath = new Uri(env.WebRootPath + @"\img\AvatarPlaceholder.png")
                });
        }

        public async Task<Image> GetImageById(Guid id)
        {
            return await db.Images.FirstOrDefaultAsync(img => img.Id == id) ?? 
                new Image 
                { 
                    ImageName = "Placeholder",
                    ImagePath = new Uri(env.WebRootPath + @"\img\ImagePlaceHolder.png")
                };
        }

        public async Task<Image> GetImageByName(string name)
        {
            return await db.Images.FirstOrDefaultAsync(img => img.ImageName == name);
        }

        public async Task<Image> RemoveImage(Image image)
        {
            db.Images.Remove(image);
            return await Task.FromResult(image);
        }

        public async Task<Image> SetAvatarImageForUser(Image image, ApplicationUser user)
        {
            user.ProfilePicture = image;
            var entity = db.Attach(user);
            entity.State = EntityState.Modified;
            return await Task.FromResult(image);
        }
    }
}
