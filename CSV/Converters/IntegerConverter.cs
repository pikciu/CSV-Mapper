using System.Diagnostics;
using System.Linq;
using CSV.Extensions;

namespace CSV.Converters
{
    internal sealed class IntegerConverter : ICsvValueConverter
    {
        public object Convert(string value)
        {
            value = value.RemoveWhiteSpaces();
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            Debug.WriteLine("Failed to parse {0} to int", new object[] { value });
            return 0;
        }

        public string ConvertBack(object value)
        {
            return value?.ToString() ?? string.Empty;
        }
    }
}