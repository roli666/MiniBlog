using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.ViewModels
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid OwnerPostId { get; set; }
        public Comment Parent { get; set; }
        public List<Comment> Children { get; set; }
        public string Content { get; set; }
        public string OwnerUser { get; set; }
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
