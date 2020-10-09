using MiniBlog.Core.Enums;

namespace MiniBlog.Core.Entities
{
    public class PublicLifeBlogPost : BlogPostBase
    {
        public PublicLifeBlogPost() : base(AgeRestrictionCategories.Felnőtt |
            AgeRestrictionCategories.Idős)
        {
        }
    }
}