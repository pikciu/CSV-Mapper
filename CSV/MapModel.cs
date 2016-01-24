using System;
using System.Collections.Generic;
using System.Reflection;
using CSV.Attributes;
using CSV.Converters;
using CSV.Exceptions;

namespace CSV
{
    internal sealed class MapModel
    {
        public MapModel(int fieldIndex, string fieldName, PropertyInfo propertyInfo, CsvConverterAttribute converterAttribute)
        {
            FieldIndex = fieldIndex;
            PropertyInfo = propertyInfo;
            FieldName = fieldName;

            if (converterAttribute != null)
            {
                ValueConverter = Activator.CreateInstance(converterAttribute.ConverterType, converterAttribute.Params) as ICsvValueConverter;
                if (ValueConverter == null)
                {
                    throw new InvalidConverterType($"Could not create instance of {converterAttribute.ConverterType.Name}. Make sure it is of type {typeof(ICsvValueConverter).Name}");
                }
            }
            else
            {
                Dictionary<Type, ICsvValueConverter> defaultConverters = DefaultConvertersFactory.Create();
                if (defaultConverters.ContainsKey(PropertyInfo.PropertyType))
                {
                    ValueConverter = defaultConverters[PropertyInfo.PropertyType];
                }
            }
        }

        public int FieldIndex { get; private set; }

        public string FieldName { get; private set; }

        public PropertyInfo PropertyInfo { get; private set; }

        public ICsvValueConverter ValueConverter { get; private set; }

        public string GetValue(object item)
        {
            object value = PropertyInfo.GetValue(item);
            if (value == null)
            {
                return string.Empty;
            }

            return ValueConverter?.ConvertBack(value) ?? value.ToString();
        }

        public void SetValue<T>(T item, string csvValue)
        {
            if (ValueConverter != null)
            {
                PropertyInfo.SetValue(item, ValueConverter.Convert(csvValue));
            }
            else
            {
                PropertyInfo.SetValue(item, csvValue);
            }
        }
    }
}