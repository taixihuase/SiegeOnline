//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：ICollection.cs
//
// 文件功能描述：
//
// 物件对象集合的通用接口，用于增删集合内对象数据
//
// 创建标识：taixihuase 20150816
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

namespace SiegeOnlineServer.Protocol.Common
{
    /// <summary>
    /// 类型：接口
    /// 名称：ICollection
    /// 作者：taixihuase
    /// 作用：对象集合接口
    /// 编写日期：2015/9/13
    /// </summary>
    interface ICollection
    {
        /// <summary>
        /// 类型：方法
        /// 名称：Add
        /// 作者：taixihuase
        /// 作用：尝试添加一个对象至指定集合索引位置
        /// 编写日期：2015/9/13
        /// </summary>
        /// <param name="index"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Add(int index, object obj);

        /// <summary>
        /// 类型：方法
        /// 名称：Remove
        /// 作者：taixihuase
        /// 作用：尝试移除指定集合索引位置的一个对象
        /// 编写日期：2015/9/13
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        bool Remove(int index);

        /// <summary>
        /// 类型：方法
        /// 名称：Clear
        /// 作者：taixihuase
        /// 作用：清空对象集合
        /// 编写日期：2015/9/13
        /// </summary>
        void Clear();
    }
}
