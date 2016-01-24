using System;

namespace CSV.Exceptions
{
    public class InvalidConverterType : Exception
    {
        public InvalidConverterType()
        {
        }

        public InvalidConverterType(string message)
            : base(message)
        {
        }
    }
}