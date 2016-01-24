namespace CSV.Converters
{
    public interface ICsvValueConverter
    {
        object Convert(string value);

        string ConvertBack(object value);
    }
}