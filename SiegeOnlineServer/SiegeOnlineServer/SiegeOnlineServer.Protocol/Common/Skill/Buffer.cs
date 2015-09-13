//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Buffer.cs
//
// 文件功能描述：
//
// Buffer 属性
//
// 创建标识：taixihuase 20150826
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using System;

namespace SiegeOnlineServer.Protocol.Common.Skill
{
    /// <summary>
    /// 类型：类
    /// 名称：Buffer
    /// 作者：taixihuase
    /// 作用：角色 Buffer 类
    /// 编写日期：2015/8/26
    /// </summary>
    [Serializable]
    public class Buffer
    {
        public string Name { get; protected set; }

        public int FixedId { get; protected set; }

        /// <summary>
        /// 类型：方法
        /// 名称：Buffer
        /// 作者：taixihuase
        /// 作用：通过数据库中获得的数据构造 Buffer 对象实例
        /// 编写日期：2015/8/27
        /// </summary>
        /// <param name="fixedId"></param>
        /// <param name="name"></param>
        public Buffer(int fixedId, string name)
        {
            FixedId = fixedId;
            Name = name;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Buffer
        /// 作者：taixihuase
        /// 作用：构造空的 Buffer 实例
        /// 编写日期：2015/8/27
        /// </summary>
        public Buffer()
        {
            FixedId = 0;
            Name = "";
        }
    }
}
