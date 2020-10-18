using MiniBlog.Core.Enums;

namespace MiniBlog.Core.Entities
{
    public class SportBlogPost : BlogPostBase
    {
        public new const string Category = "Sport";
        public SportBlogPost() : base(AgeRestrictionCategories.Felnőtt, Category)
        {
        }
    }
}