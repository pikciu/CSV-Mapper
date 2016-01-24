using System;
using System.Diagnostics;
using System.Globalization;

namespace CSV.Converters
{
    public sealed class DateTimeConverter : ICsvValueConverter
    {
        public DateTimeConverter(string dateFormat)
        {
            DateFormat = dateFormat;
        }

        public string DateFormat { get; }

        public object Convert(string value)
        {
            DateTime date;
            if (DateTime.TryParseExact(value, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return date;
            }
            Debug.WriteLine("Failed to parse {0} to DateTime", new object[] { value });
            return default(DateTime);
        }

        public string ConvertBack(object value)
        {
            if (value is DateTime)
            {
                return ((DateTime)value).ToString(DateFormat);
            }

            return string.Empty;
        }
    }
}