

// ReSharper disable CheckNamespace

using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SiegeOnlineClient.Component
{
    public static class StringTool
    {
        public static bool IsNumber(string str)
        {
            return Regex.IsMatch(str, @"^[0-9]+$");
        }

        public static bool IsLetter(string str)
        {
            return Regex.IsMatch(str, @"^[A-Za-z]+$");
        }

        public static bool IsLetterOrNumber(string str)
        {
            return Regex.IsMatch(str, @"(?i)^[0-9a-z]+$");
        }

        public static int CountChinese(string str)
        {
            return str.Count(c => Regex.IsMatch(c.ToString(), @"^[\u4E00-\u9FA5]{0,}$"));
        }

        public static bool IsChinese(string str)
        {
            return Regex.IsMatch(str, @"^[\u4e00-\u9fa5],{0,}$");
        }

        public static bool HasChinese(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }

        public static int CountSbcCase(string str)
        {
            return Encoding.Default.GetByteCount(str) - str.Length;
        }

        public static bool HasSbcCase(string str)
        {
            return CountSbcCase(str) > 0;
        }

        public static int CountDbcCase(string str)
        {
            return str.Length - CountSbcCase(str);
        }

        public static bool HasDbcCase(string str)
        {
            return CountDbcCase(str) > 0;
        }

        public static bool IsEmail(string str)
        {
            return Regex.IsMatch(str, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }
    }
}
