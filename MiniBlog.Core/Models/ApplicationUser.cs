#nullable enable
using Microsoft.AspNetCore.Identity;
using MiniBlog.Core.Entities;
using System;

namespace MiniBlog.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public DateTime Born { get; set; }

        public Guid? ProfilePictureId { get; set; }
        [PersonalData]
        public Image? ProfilePicture { get; set; }
    }
}