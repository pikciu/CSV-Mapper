using System;

namespace CSV.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CsvPropertyAttribute : Attribute
    {
        public CsvPropertyAttribute(string name, int order = 0)
        {
            Name = name;
            Order = order;
        }

        public string Name { get; private set; }

        public int Order { get; private set; }
    }
}