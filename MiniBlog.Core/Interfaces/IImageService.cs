using MiniBlog.Core.Entities;
using MiniBlog.Core.Models;
using System;
using System.Threading.Tasks;

namespace MiniBlog.Core.Interfaces
{
    public interface IImageService
    {
        Task<Image> GetUserAvatar(ApplicationUser user);
        Task<Image> GetImageById(Guid imageId);
    }
}