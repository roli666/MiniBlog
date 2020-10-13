using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.ViewModels
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Category { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<string> AllowedAges { get; set; }
        public string BackgroundImage { get; set; }
    }
}
