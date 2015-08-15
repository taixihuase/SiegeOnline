//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Jewel.cs
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
using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace SiegeOnlineServer.Protocol.Common.Item
{
    /// <summary>
    /// 类型：类
    /// 名称：Jewel
    /// 作者：taixihuase
    /// 作用：首饰类
    /// 编写日期：2015/7/23
    /// </summary>
    [Serializable]
    public class Jewel
    {
        public int FixedId { get; protected set; }

        public int AllocatedId { get; protected set; }

        public string Name { get; protected set; }

        #region 饰品类型

        [Serializable]
        public enum JewelType : byte
        {

        }

        public byte Type { get; protected set; }

        #endregion

        public Dictionary<AttributeCode, int> AbilityPoints; // 防御力上下限

        public Jewel(int guid, int allocatedId, string name, JewelType type)
        {
            FixedId = guid;
            AllocatedId = allocatedId;
            Name = name;
            Type = (byte) type;
            AbilityPoints = new Dictionary<AttributeCode, int>();
        }

        public Jewel()
        {
            FixedId = 0;
            AllocatedId = 0;
            Name = "";
            Type = 0;
            AbilityPoints = new Dictionary<AttributeCode, int> {{AttributeCode.Null, 0}};
        }
    }
}
