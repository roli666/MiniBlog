using MiniBlog.Core.Enums;

namespace MiniBlog.Core.Entities
{
    public class PublicLifeBlogPost : BlogPostBase
    {
        public new const string Category = "Public Life";
        public PublicLifeBlogPost() : base(AgeRestrictionCategories.Felnőtt |
            AgeRestrictionCategories.Idős,Category)
        {
        }
    }
}