using Microsoft.AspNetCore.Identity;
using MiniBlog.Core.Entities;
using MiniBlog.Core.Interfaces;
using MiniBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Core.Services
{
    public class FileService : IFileService
    {
        public FileService()
        {

        }

        public Task<Image> SaveImage(Stream file)
        {
            throw new NotImplementedException();
        }
    }
}
