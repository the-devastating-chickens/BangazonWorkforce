using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.IntegrationTests.Helpers
{
    public static class StringHelpers
    {
        public static string EnsureMaxLength(string str, int maxLength)
        {
            if (str == null)
            {
                return null;
            }
            if (str.Length <= maxLength)
            {
                return str;
            }
            return str.Substring(0, maxLength);
        }
    }
}
