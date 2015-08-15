//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Serialization.cs
//
// 文件功能描述：
//
// 数据对象二进制序列化及反序列化
//
// 创建标识：taixihuase 20150714
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SiegeOnlineServer.Protocol
{
    /// <summary>
    /// 类型：类
    /// 名称：Serialization
    /// 作者：taixihuase
    /// 作用：对数据进行二进制序列化与反序列化
    /// 编写日期：2015/7/14
    /// </summary>
    public class Serialization
    {
        /// <summary>
        /// 类型：方法
        /// 名称：Serialize
        /// 作者：taixihuase
        /// 作用：将一个对象二进制序列化
        /// 编写日期：2015/7/14
        /// </summary>
        /// <param name="unSerializedObj"></param>
        /// <returns></returns>
        public static byte[] Serialize(object unSerializedObj)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, unSerializedObj);
            return stream.ToArray();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Deserialize
        /// 作者：taixihuase
        /// 作用：将一个二进制序列化数据流反序列化为一个对象
        /// 编写日期：2015/7/14
        /// </summary>
        /// <param name="serializedArray"></param>
        /// <returns></returns>
        public static object Deserialize(object serializedArray)
        {
            MemoryStream stream = new MemoryStream((byte[]) serializedArray);
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            object unSerializedObj = formatter.Deserialize(stream);
            return unSerializedObj;
        }
    }
}
