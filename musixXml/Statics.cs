using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;

namespace musicXml
{
    public static class Statics
    {
        public static string GetDescription(object enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());
            if (null != fi)
            {
                object[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return "";
        }
    }
}
