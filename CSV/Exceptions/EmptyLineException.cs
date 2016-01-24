using System;

namespace CSV.Exceptions
{
    public sealed class EmptyLineException : Exception
    {
        public EmptyLineException()
        {
        }

        public EmptyLineException(string message)
            : base(message)
        {
        }
    }
}