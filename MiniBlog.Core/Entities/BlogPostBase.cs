using MiniBlog.Core.Enums;
using MiniBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiniBlog.Core.Entities
{
    public abstract class BlogPostBase
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public AgeRestrictionCategories AllowedAge { get; set; }
        public Image BackgroundImage { get; set; }
        public abstract string Category { get; }

        public BlogPostBase(AgeRestrictionCategories allowedAges)
        {
            AllowedAge = allowedAges;
            CreatedOn = DateTime.Now;
        }
    }
}