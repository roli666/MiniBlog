using System;

namespace MiniBlog.Core.Enums
{
    [Flags]
    public enum AgeRestrictionCategories
    {
        Gyerek = 1,
        Felnőtt = 2,
        Idős = 4
    }
}