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
    class CustomInt
    {
        int _value;
        bool _readonly;
        List<int> _allowlist;

        public int Value { get { return _value; } set { _value = value; } }
        public bool ReadOnly { get { return _readonly; } set { _readonly = value; } }
        public List<int> AllowList { get { return _allowlist; } set { _allowlist = value; } }

        public CustomInt()
        {
            _allowlist = new List<int>();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    class CustomIntConverter : TypeConverter
    {
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var x = context.PropertyDescriptor.GetValue(context.Instance);
            if (x != null && x is CustomInt)
            {
                return new StandardValuesCollection((x as CustomInt).AllowList);
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
            if (x is CustomInt && value is CustomInt)
            {
                (x as CustomInt).Value = (value as CustomInt).Value;
                return (CustomInt)x;
            }
            else if (x is CustomInt && value is string)
            {
                (x as CustomInt).Value = int.Parse((string)value);
                return (CustomInt)x;
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
    }
}
