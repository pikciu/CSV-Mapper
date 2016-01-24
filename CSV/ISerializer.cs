using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CSV
{
    public interface ISerializer<in T>
    {
        string Seperator { get; set; }

        Task<Stream> SerializeAsync(IEnumerable<T> items);
    }
}