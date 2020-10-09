using MiniBlog.Core.Entities;
using MiniBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Core.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<Image> AddImage(Image image);
        Task<Image> GetImageById(Guid id);
        Task<Image> GetAvatarImageForUser(ApplicationUser user);
        Task<Image> SetAvatarImageForUser(Image image, ApplicationUser user);
        Task<Image> GetImageByName(string name);
        Task<Image> RemoveImage(Image image);
    }
}
