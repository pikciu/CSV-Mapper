using System.Diagnostics;
using System.Globalization;
using CSV.Extensions;

namespace CSV.Converters
{
    internal sealed class DoubleConverter : ICsvValueConverter
    {
        public object Convert(string value)
        {
            value = value.RemoveWhiteSpaces().Replace(",", ".");
            double result;
            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                return result;
            }

            Debug.WriteLine("Failed to parse {0} to double", new object[] { value });
            return 0;
        }

        public string ConvertBack(object value)
        {
            return value?.ToString() ?? string.Empty;
        }
    }
}