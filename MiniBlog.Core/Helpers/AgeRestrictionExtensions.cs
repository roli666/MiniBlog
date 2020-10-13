using MiniBlog.Core.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MiniBlog.Core.Helpers
{
    public static class AgeRestrictionExtensions
    {
        public static IEnumerable<string> GetAgeRestrictions(this AgeRestrictionCategories categories) 
        {
            List<string> result = new List<string>();
            foreach (AgeRestrictionCategories r in Enum.GetValues(typeof(AgeRestrictionCategories)))
            {
                if ((categories & r) != 0) result.Add(r.ToString());
            }
            return result;
        }
    }
}
