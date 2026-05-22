using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonList.Helper
{
    internal static class FloatHelper
    {
        public static int GetDecimalPlace(this float value)
        {
            var split = value.ToString().Split('.');
            if (split.Length == 1)
                return 0;
            else
                return split[1].Length;
        }
    }
}
