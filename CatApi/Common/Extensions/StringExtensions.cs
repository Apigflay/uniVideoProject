using System;
using System.Globalization;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static string ExTrimPrefix(this string source, string prefix, bool ignoreCase = false)
        {
            prefix = prefix ?? string.Empty;
            if (!source.StartsWith(prefix, ignoreCase, CultureInfo.CurrentCulture))
            {
                return source;
            }
            return source.Remove(0, prefix.Length);
        }

        public static string ExTrimSuffix(this string source, string suffix, bool ignoreCase = false)
        {
            suffix = suffix.Trim() ?? string.Empty;
            if (!source.EndsWith(suffix, ignoreCase, CultureInfo.CurrentCulture))
            {
                return source;
            }
            return source.Substring(0, source.Length - suffix.Length);
        }
    }
}
