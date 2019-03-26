using System;
using System.Collections.Generic;
using System.Text;

namespace Knight.Nuget.Lib
{
    public static class StringHelper
    {
        public static string AppendKnight(this string value)
        {
            return string.Format($"{value}_Knight");
        }
    }
}
