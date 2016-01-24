using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CSV
{
    public interface IMapper<T>
    {
        string Seperator { get; set; }

        Task<IEnumerable<T>> MapAsync(StreamReader stream);
    }
}