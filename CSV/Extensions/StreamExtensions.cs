using System.IO;
using System.Text;

namespace CSV.Extensions
{
    internal static class StreamExtensions
    {
        public static void WriteString(this MemoryStream stream, string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}