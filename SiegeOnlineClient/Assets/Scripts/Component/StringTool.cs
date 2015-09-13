//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：StringTool.cs
//
// 文件功能描述：
//
// 静态字符串工具类，用于对字符串进行判断和操作
//
// 创建标识：taixihuase 20150905
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
// ReSharper disable CheckNamespace

namespace SiegeOnlineClient.Component
{
    /// <summary>
    /// 类型：类
    /// 名称：StringTool
    /// 作者：taixihuase
    /// 作用：字符串工具类
    /// 编写日期：2015/9/5
    /// </summary>
    public static class StringTool
    {
        /// <summary>
        /// 类型：方法
        /// 名称：IsNumber
        /// 作者：taixihuase
        /// 作用：判断字符串是否纯数字
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(string str)
        {
            return Regex.IsMatch(str, @"^[0-9]+$");
        }

        /// <summary>
        /// 类型：方法
        /// 名称：IsLetter
        /// 作者：taixihuase
        /// 作用：判断字符串是否纯字母
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLetter(string str)
        {
            return Regex.IsMatch(str, @"^[A-Za-z]+$");
        }

        /// <summary>
        /// 类型：方法
        /// 名称：IsLetterOrNumber
        /// 作者：taixihuase
        /// 作用：判断字符串是否字母或数字的组合
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLetterOrNumber(string str)
        {
            return Regex.IsMatch(str, @"(?i)^[0-9a-z]+$");
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CountChinese
        /// 作者：taixihuase
        /// 作用：统计字符串中汉字个数
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int CountChinese(string str)
        {
            return str.Count(c => Regex.IsMatch(c.ToString(), @"^[\u4E00-\u9FA5]{0,}$"));
        }

        /// <summary>
        /// 类型：方法
        /// 名称：IsChinese
        /// 作者：taixihuase
        /// 作用：判断字符串是否纯中文
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsChinese(string str)
        {
            return Regex.IsMatch(str, @"^[\u4e00-\u9fa5],{0,}$");
        }

        /// <summary>
        /// 类型：方法
        /// 名称：HasChinese
        /// 作者：taixihuase
        /// 作用：判断字符串中是否包含中文
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HasChinese(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CountSbcCase
        /// 作者：taixihuase
        /// 作用：统计字符串中全角字符个数
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int CountSbcCase(string str)
        {
            return Encoding.Default.GetByteCount(str) - str.Length;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：HasSbcCase
        /// 作者：taixihuase
        /// 作用：判断字符串中是否包含全角字符
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HasSbcCase(string str)
        {
            return CountSbcCase(str) > 0;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CountDbcCase
        /// 作者：taixihuase
        /// 作用：统计字符串中半角字符个数
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int CountDbcCase(string str)
        {
            return str.Length - CountSbcCase(str);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：HasDbcCase
        /// 作者：taixihuase
        /// 作用：判断字符串中是否包含半角字符
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HasDbcCase(string str)
        {
            return CountDbcCase(str) > 0;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：IsEmail
        /// 作者：taixihuase
        /// 作用：判断字符串中是否符合邮箱格式
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(string str)
        {
            return Regex.IsMatch(str, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }
    }
}
