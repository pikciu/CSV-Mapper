using System;

namespace CSV.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CsvFieldsSeperatorAttribute : Attribute
    {
        public CsvFieldsSeperatorAttribute(string seperator)
        {
            Seperator = seperator;
        }

        public string Seperator { get; private set; }
    }
}