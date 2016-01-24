using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CSV.Attributes;
using CSV.Exceptions;

namespace CSV
{
    public sealed class Mapper<T> : IMapper<T>
    {
        private string[] _seperator;

        public Mapper()
        {
            var seperatorAttribute = typeof(T).GetCustomAttribute<CsvFieldsSeperatorAttribute>();
            if (seperatorAttribute == null)
            {
                _seperator = new[] { "," };
            }
            else
            {
                _seperator = new[] { seperatorAttribute.Seperator };
            }
        }

        public Mapper(string seperator)
        {
            _seperator = new[] { seperator };
        }

        public string Seperator
        {
            get { return _seperator[0]; }
            set { _seperator = new[] { value }; }
        }

        public Task<IEnumerable<T>> MapAsync(StreamReader stream)
        {
            List<MapModel> maps = CreateFieldsMap(stream);
            if (maps.Count == 0)
            {
                return Task.FromResult(new List<T>().AsEnumerable());
            }

            return Task.Run(() =>
            {
                var result = new List<T>();
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] values = line.Split(_seperator, StringSplitOptions.None);
                    var model = Activator.CreateInstance<T>();
                    foreach (MapModel map in maps)
                    {
                        if (map.FieldIndex < values.Length)
                        {
                            string csvValue = values[map.FieldIndex];
                            map.SetValue(model, csvValue);
                        }
                    }
                    result.Add(model);
                }
                return result.AsEnumerable();
            });
        }

        private List<MapModel> CreateFieldsMap(StreamReader stream)
        {
            string line = stream.ReadLine();
            if (line == null)
            {
                throw new EmptyLineException("Current position of stream does not contains any text line");
            }
            List<string> csvFields = line.Split(_seperator, StringSplitOptions.None).ToList();
            List<MapModel> map = typeof(T).GetProperties().Select(s => new
            {
                Property = s,
                CsvAttribute = s.GetCustomAttribute<CsvPropertyAttribute>(),
                Converter = s.GetCustomAttribute<CsvConverterAttribute>()
            }).Where(p => p.CsvAttribute != null && csvFields.Contains(p.CsvAttribute.Name))
                                          .Select(s => new MapModel(csvFields.IndexOf(s.CsvAttribute.Name), s.CsvAttribute.Name, s.Property, s.Converter)).ToList();
            return map;
        }
    }
}