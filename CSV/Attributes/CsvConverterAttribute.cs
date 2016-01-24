using System;

namespace CSV.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CsvConverterAttribute : Attribute
    {
        public CsvConverterAttribute(Type converterType, params object[] parameters)
        {
            ConverterType = converterType;
            Params = parameters;
        }

        public Type ConverterType { get; private set; }
        public object[] Params { get; private set; }
    }
}