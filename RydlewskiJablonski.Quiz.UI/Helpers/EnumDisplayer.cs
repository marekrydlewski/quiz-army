using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Data;
using System.Windows.Markup;
using RydlewskiJablonski.Quiz.Core.Helpers;

namespace RydlewskiJablonski.Quiz.UI.Helpers
{
    [ContentProperty("OverriddenDisplayEntries")]
    public class EnumDisplayer : IValueConverter
    {
        private Type _type;
        private IDictionary _displayValues;
        private IDictionary _reverseValues;
        private List<EnumDisplayEntry> _overriddenDisplayEntries;

        public EnumDisplayer()
        {
        }

        public EnumDisplayer(Type type)
        {
            _type = type;
        }

        public Type Type
        {
            get { return _type; }
            set
            {
                if (!value.IsEnum)
                    throw new ArgumentException("parameter is not an Enumermated type", "value");
                _type = value;
            }
        }

        public ReadOnlyCollection<string> DisplayNames
        {
            get
            {
                Type displayValuesType = typeof(Dictionary<,>)
                    .GetGenericTypeDefinition().MakeGenericType(_type, typeof(string));
                _displayValues = (IDictionary) Activator.CreateInstance(displayValuesType);

                _reverseValues =
                    (IDictionary) Activator.CreateInstance(typeof(Dictionary<,>)
                        .GetGenericTypeDefinition()
                        .MakeGenericType(typeof(string), _type));

                var fields = _type.GetFields(BindingFlags.Public | BindingFlags.Static);
                foreach (var field in fields)
                {
                    DisplayStringAttribute[] a = (DisplayStringAttribute[])
                        field.GetCustomAttributes(typeof(DisplayStringAttribute), false);

                    string displayString = GetDisplayStringValue(a);
                    object enumValue = field.GetValue(null);

                    if (displayString == null)
                    {
                        displayString = GetBackupDisplayStringValue(enumValue);
                    }
                    if (displayString != null)
                    {
                        _displayValues.Add(enumValue, displayString);
                        _reverseValues.Add(displayString, enumValue);
                    }
                }
                return new List<string>((IEnumerable<string>) _displayValues.Values).AsReadOnly();
            }
        }

        private string GetDisplayStringValue(DisplayStringAttribute[] a)
        {
            if (a == null || a.Length == 0) return null;
            DisplayStringAttribute dsa = a[0];
            if (!string.IsNullOrEmpty(dsa.ResourceKey))
            {
                ResourceManager rm = new ResourceManager(_type);
                return rm.GetString(dsa.ResourceKey);
            }
            return dsa.Value;
        }

        private string GetBackupDisplayStringValue(object enumValue)
        {
            if (_overriddenDisplayEntries != null && _overriddenDisplayEntries.Count > 0)
            {
                EnumDisplayEntry foundEntry = _overriddenDisplayEntries.Find(delegate(EnumDisplayEntry entry)
                {
                    object e = Enum.Parse(_type, entry.EnumValue);
                    return enumValue.Equals(e);
                });
                if (foundEntry != null)
                {
                    if (foundEntry.ExcludeFromDisplay) return null;
                    return foundEntry.DisplayString;
                }
            }
            return Enum.GetName(_type, enumValue);
        }

        public List<EnumDisplayEntry> OverriddenDisplayEntries
        {
            get { return _overriddenDisplayEntries ?? (_overriddenDisplayEntries = new List<EnumDisplayEntry>()); }
        }


        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return
                _displayValues
                    [value]
                ;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)

        {
            return
                _reverseValues
                    [value]
                ;
        }
    }
}