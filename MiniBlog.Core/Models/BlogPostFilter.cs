using MiniBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MiniBlog.Core.Models
{
    public class BlogPostFilter
    {
        public int? Page { get; set; } = 0;
        public int? Limit { get; set; } = 10;
        public string ByUser { get; set; }
        public string ByCategory { get; set; }
        public DateTime? InMonth{ get; set; }
    }
}
