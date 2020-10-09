using MiniBlog.Core.Entities;
using System.IO;
using System.Threading.Tasks;

namespace MiniBlog.Core.Interfaces
{
    public interface IFileService
    {
        Task<Image> SaveImage(Stream file);
    }
}
