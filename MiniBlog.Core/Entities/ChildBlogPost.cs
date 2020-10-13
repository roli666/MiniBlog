using MiniBlog.Core.Enums;

namespace MiniBlog.Core.Entities
{
    public class ChildBlogPost : BlogPostBase
    {
        public ChildBlogPost() : base(
            AgeRestrictionCategories.Felnőtt |
            AgeRestrictionCategories.Gyerek |
            AgeRestrictionCategories.Idős)
        { }

        public override string Category => "Gyerek";
    }
}