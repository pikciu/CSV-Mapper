using System.Linq;

namespace CSV.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveWhiteSpaces(this string value)
        {
            return new string(value.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }
    }
}