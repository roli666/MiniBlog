using MiniBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniBlog.Core.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public ApplicationUser OwnerUser { get; set; }
        public Guid OwnerPostId { get; set; }
        public BlogPostBase OwnerPost { get; set; }
        public Comment Parent { get; set; }
        public List<Comment> Children { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 255, MinimumLength = 1)]
        public string Content { get; set; }

        [NotMapped]
        public int Depth { get; private set; }

        public Comment()
        {
            SetDepth();
        }

        private void SetDepth()
        {
            int depth = 0;
            Comment parent = Parent;
            while (parent != null)
            {
                depth++;
                parent = parent.Parent;
            }
            Depth = depth;
        }
    }
}