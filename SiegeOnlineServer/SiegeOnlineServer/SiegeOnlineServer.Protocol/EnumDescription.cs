using System;
using System.ComponentModel;
// ReSharper disable PossibleNullReferenceException

namespace SiegeOnlineServer.Protocol
{
    public static class EnumDescription
    {
        public static string GetEnumDescription<TEnum>(object value)
        {
            Type enumType = typeof(TEnum);
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("enumItem requires a Enum ");
            }
            var name = Enum.GetName(enumType, Convert.ToInt32(value));
            if (name == null)
                return string.Empty;
            object[] objs = enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs.Length == 0)
            {
                return string.Empty;
            }
            DescriptionAttribute attr = objs[0] as DescriptionAttribute;
            return attr.Description;
        }
    }
}
