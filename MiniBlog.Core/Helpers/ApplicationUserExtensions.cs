using MiniBlog.Core.Enums;
using MiniBlog.Core.Models;
using System;

namespace MiniBlog.Core.Helpers
{
    public static class ApplicationUserExtensions
    {
        public static AgeRestrictionCategories GetAgeRestrictionCategories(this ApplicationUser user)
        {
            var today = DateTime.Now;
            var userAge = today.Year - user.Born.Year;
            if (user.Born.Date > today.AddYears(-userAge)) //leap year check
                userAge--;
            if (userAge > 60)
                return AgeRestrictionCategories.Idős;
            if (userAge >= 18)
                return AgeRestrictionCategories.Felnőtt;
            return AgeRestrictionCategories.Gyerek;
        }
    }
}
