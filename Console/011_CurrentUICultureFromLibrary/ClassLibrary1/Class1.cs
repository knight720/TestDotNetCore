using System;
using System.Globalization;

namespace ClassLibrary1
{
    public class Class1
    {
        public static string GetLocale()
        {
            return CultureInfo.CurrentUICulture.Name;
        }
    }
}
