//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Buffer.cs
//
// 文件功能描述：
//
// Buffer属性
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
    /// 作用：角色Buffer类
    /// 编写日期：2015/8/26
    /// </summary>
    [Serializable]
    public class Buffer
    {
        public string Name { get; protected set; }

        public int FixedId { get; protected set; }

        public Buffer(int fixedId, string name)
        {
            FixedId = fixedId;
            Name = name;
        }

        public Buffer()
        {
            FixedId = 0;
            Name = "";
        }
    }
}
