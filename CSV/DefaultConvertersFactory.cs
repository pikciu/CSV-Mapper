using System;
using System.Collections.Generic;
using CSV.Converters;

namespace CSV
{
    internal class DefaultConvertersFactory
    {
        public static Dictionary<Type, ICsvValueConverter> Create()
        {
            return new Dictionary<Type, ICsvValueConverter>()
            {
                { typeof(int), new IntegerConverter() },
                { typeof(double), new DoubleConverter() }
            };
        }
    }
}