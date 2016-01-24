using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CSV.Attributes;
using CSV.Extensions;

namespace CSV
{
    public class Serializer<T> : ISerializer<T>
    {
        public Serializer()
        {
            var seperatorAttribute = typeof(T).GetCustomAttribute<CsvFieldsSeperatorAttribute>();
            if (seperatorAttribute == null)
            {
                Seperator = ",";
            }
            else
            {
                Seperator = seperatorAttribute.Seperator;
            }
        }

        public Serializer(string seperator)
        {
            Seperator = seperator;
        }

        public string Seperator { get; set; }

        public Task<Stream> SerializeAsync(IEnumerable<T> items)
        {
            return Task.Run(() =>
            {
                List<MapModel> maps = CreateMaps();
                var stream = new MemoryStream();

                if (maps.Count > 0)
                {
                    string header = string.Join(Seperator, maps.Select(s => s.FieldName));
                    stream.WriteString(header);
                    stream.WriteString(Environment.NewLine);
                    foreach (T item in items)
                    {
                        string csvItem = string.Join(Seperator, maps.Select(s => s.GetValue(item)));
                        stream.WriteString(csvItem);
                        stream.WriteString(Environment.NewLine);
                    }
                }
                stream.Seek(0, SeekOrigin.Begin);
                return (Stream)stream;
            });
        }

        private List<MapModel> CreateMaps()
        {
            return typeof(T).GetRuntimeProperties().Select(s => new
            {
                Property = s,
                CsvAttribute = s.GetCustomAttribute<CsvPropertyAttribute>(),
                CsvConverter = s.GetCustomAttribute<CsvConverterAttribute>()
            }).Where(p => p.CsvAttribute != null)
                            .OrderBy(o => o.CsvAttribute.Order)
                            .Select((s, i) => new MapModel(i, s.CsvAttribute.Name, s.Property, s.CsvConverter)).ToList();
        }
    }
}