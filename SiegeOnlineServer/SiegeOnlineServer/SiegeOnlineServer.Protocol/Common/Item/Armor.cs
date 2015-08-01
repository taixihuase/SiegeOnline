//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Armor.cs
//
// 文件功能描述：
//
// 防具属性
//
// 创建标识：taixihuase 20150723
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

namespace SiegeOnlineServer.Protocol.Common.Item
{
    /// <summary>
    /// 类型：类
    /// 名称：Armor
    /// 作者：taixihuase
    /// 作用：防具类
    /// 编写日期：2015/7/23
    /// </summary>
    [Serializable]
    public class Armor
    {
        public string Name { get; protected set; }

        public Armor(string name)
        {
            Name = name;
        }
    }
}
