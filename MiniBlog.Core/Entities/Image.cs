using System;

namespace MiniBlog.Core.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public string ImageName { get; set; }
        public Uri ImagePath { get; set; }
    }
}