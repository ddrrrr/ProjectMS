using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMS.Core.PropertyBase
{
    [Serializable]
    class CustomString
    {
        string _value;
        bool _readonly;
        List<string> _allowlist;
        public event Action<string> ValueChange;

        public string Value { get { return _value; } set { _value = value; ValueChange?.Invoke(value); } }
        public bool ReadOnly { get { return _readonly; } set { _readonly = value; } }
        public List<string> AllowList { get { return _allowlist; } set { _allowlist = value; } }

        public CustomString()
        {
            _allowlist = new List<string>();
        }

        public override string ToString()
        {
            return Value;
        }
    }

    class CustomStringConverter : TypeConverter
    {
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var x = context.PropertyDescriptor.GetValue(context.Instance);
            if (x != null && x is CustomString)
            {
                return new StandardValuesCollection((x as CustomString).AllowList);
            }
            return base.GetStandardValues(context);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var x = context.PropertyDescriptor.GetValue(context.Instance);
            if (x is CustomString && value is CustomString)
            {
                (x as CustomString).Value = (value as CustomString).Value;
                return (CustomString)x;
            }
            else if (x is CustomString && value is string)
            {
                (x as CustomString).Value = (string)value;
                return (CustomString)x;
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
    }
}
