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
    /// 作用：饰品类
    /// 编写日期：2015/7/23
    /// </summary>
    [Serializable]
    public class Jewel
    {
        public int FixedId { get; protected set; }              // 数据库固定编号

        public int AllocatedId { get; protected set; }          // 动态分配编号

        public string Name { get; protected set; }

        public int MaxLevel { get; protected set; }

        public int LevelLimit { get; protected set; }

        public int CurrentLevel { get; set; }

        #region 饰品类型

        [Serializable]
        public enum JewelType : byte
        {
            Null,
        }

        public byte Type { get; protected set; }

        #endregion

        public Dictionary<AttributeCode, int> AbilityPoints; // 防御力上下限

        public Jewel(int guid, int allocatedId, string name, int limit, int max, int cur, JewelType type)
        {
            FixedId = guid;
            AllocatedId = allocatedId;
            Name = name;
            LevelLimit = limit;
            MaxLevel = max;
            CurrentLevel = cur;
            Type = (byte) type;
            AbilityPoints = new Dictionary<AttributeCode, int>();
        }

        public Jewel()
        {
            FixedId = 0;
            AllocatedId = 0;
            Name = "";
            LevelLimit = 0;
            MaxLevel = 0;
            CurrentLevel = 0;
            Type = (byte) JewelType.Null;
            AbilityPoints = new Dictionary<AttributeCode, int> {{AttributeCode.Null, 0}};
        }
    }
}
