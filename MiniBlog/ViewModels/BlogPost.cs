using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniBlog.ViewModels
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 500, MinimumLength = 10)]
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [Required]
        public string Category { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<string> AllowedAges { get; set; }
        public string BackgroundImage { get; set; }
        public int CommentCount { get; set; }
    }
}
