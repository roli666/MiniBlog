using MiniBlog.Core.Enums;

namespace MiniBlog.Core.Entities
{
    public class SportBlogPost : BlogPostBase
    {
        public SportBlogPost() : base(AgeRestrictionCategories.Felnőtt)
        {
        }
    }
}