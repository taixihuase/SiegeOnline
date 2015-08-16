//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Equipment.cs
//
// 文件功能描述：
//
// 装备抽象基类，定义了装备的共用属性及方法
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

using System;
using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace SiegeOnlineServer.Protocol.Common.Item.Equipment
{
    /// <summary>
    /// 类型：类
    /// 名称：Equipment
    /// 作者：taixihuase
    /// 作用：装备抽象基类
    /// 编写日期：2015/8/16
    /// </summary>
    [Serializable]
    public class Equipment
    {
        public int FixedId { get; protected set; } // 数据库固定编号

        public int AllocatedId { get; protected set; } // 动态分配编号

        public string Name { get; protected set; }

        public byte Occupation { get; protected set; }

        public int MaxLevel { get; protected set; }

        public int LevelLimit { get; protected set; }

        public int CurrentLevel { get; set; }

        #region 装备类型

        [Serializable]
        public enum EquipmentType : byte
        {
            Null,
            Armor,
            Jewel,
            Weapon
        }

        public byte EquipType { get; protected set; }

        #endregion

        public List<KeyValuePair<AttributeCode, int>> FixedAttributes;      // 固定属性（多条）
        public KeyValuePair<AttributeCode, int> RandomAttribute;            // 随机属性（单条）

        public Equipment(int @fixed, int allocated, string name, OccupationCode occupation, int limit, int max,
            int cur, EquipmentType type)
        {
            FixedId = @fixed;
            AllocatedId = allocated;
            Name = name;
            Occupation = (byte) occupation;
            LevelLimit = limit;
            MaxLevel = max;
            CurrentLevel = cur;
            EquipType = (byte) type;
            FixedAttributes = new List<KeyValuePair<AttributeCode, int>>
            {
                new KeyValuePair<AttributeCode, int>(AttributeCode.Null, 0)
            };
            RandomAttribute = new KeyValuePair<AttributeCode, int>(AttributeCode.Null, 0);
        }

        public Equipment(Equipment equipment)
        {
            FixedId = equipment.FixedId;
            AllocatedId = equipment.AllocatedId;
            Name = equipment.Name;
            Occupation = equipment.Occupation;
            LevelLimit = equipment.LevelLimit;
            MaxLevel = equipment.MaxLevel;
            CurrentLevel = equipment.CurrentLevel;
            EquipType = equipment.EquipType;
            FixedAttributes = equipment.FixedAttributes;
            RandomAttribute = equipment.RandomAttribute;
        }

        public Equipment()
        {
            FixedId = 0;
            AllocatedId = 0;
            Name = "";
            Occupation = (byte) OccupationCode.Common;
            LevelLimit = 0;
            MaxLevel = 0;
            CurrentLevel = 0;
            EquipType = (byte) EquipmentType.Null;
            FixedAttributes = new List<KeyValuePair<AttributeCode, int>>
            {
                new KeyValuePair<AttributeCode, int>(AttributeCode.Null, 0)
            };
            RandomAttribute = new KeyValuePair<AttributeCode, int>(AttributeCode.Null, 0);
        }
    }
}
