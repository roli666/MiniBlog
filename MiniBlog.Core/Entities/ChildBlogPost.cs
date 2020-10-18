using MiniBlog.Core.Enums;

namespace MiniBlog.Core.Entities
{
    public class ChildBlogPost : BlogPostBase
    {
        public new const string Category = "Gyerek";
        public ChildBlogPost() : base(
            AgeRestrictionCategories.Felnőtt |
            AgeRestrictionCategories.Gyerek |
            AgeRestrictionCategories.Idős, Category)
        { }
    }
}