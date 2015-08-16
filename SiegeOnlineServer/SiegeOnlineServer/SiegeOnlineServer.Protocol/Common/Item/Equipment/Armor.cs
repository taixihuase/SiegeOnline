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
using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace SiegeOnlineServer.Protocol.Common.Item.Equipment
{
    /// <summary>
    /// 类型：类
    /// 名称：Armor
    /// 作者：taixihuase
    /// 作用：防具类
    /// 编写日期：2015/7/23
    /// </summary>
    [Serializable]
    public class Armor : Equipment, IEquipment
    {
        #region 防具类型

        [Serializable]
        public enum ArmorType : byte
        {
            Null,
            Armor,
            Boots,
            Gloves,
            Helmet,
        }

        public byte Type { get; protected set; }

        #endregion

        public int[] DefensePoints { get; set; }

        public Dictionary<int, KeyValuePair<AttributeCode, int>> ForgingAttributes; // 锻造附加属性

        public Armor(int fixedId, int allocatedId, string name, int limit, int max, int cur, ArmorType type)
        {
            FixedId = fixedId;
            AllocatedId = allocatedId;
            Name = name;
            LevelLimit = limit;
            MaxLevel = max;
            CurrentLevel = cur;
            Type = (byte) type;
            DefensePoints = new[] {0, 0};
            ForgingAttributes = new Dictionary<int, KeyValuePair<AttributeCode, int>>
            {
                {-1, new KeyValuePair<AttributeCode, int>(AttributeCode.Null, 0)}
            };
        }

        public Armor()
        {
            FixedId = 0;
            AllocatedId = 0;
            Name = "";
            LevelLimit = 0;
            MaxLevel = 0;
            CurrentLevel = 0;
            DefensePoints = new[] {0, 0};
            ForgingAttributes = new Dictionary<int, KeyValuePair<AttributeCode, int>>
            {
                {-1, new KeyValuePair<AttributeCode, int>(AttributeCode.Null, 0)}
            };
        }

        public void UpdateDefensePoints(int physical, int magic)
        {
            if (DefensePoints.Count() >= 0)
            {
                DefensePoints[0] = physical;
                DefensePoints[1] = magic;
            }
        }

        public void UpdateForgingAttribute(int level, AttributeCode attribute, int value)
        {
            if (ForgingAttributes.ContainsKey(level))
            {
                ForgingAttributes[level] = new KeyValuePair<AttributeCode, int>(attribute, value);
            }
        }

        public void UpgradeForgingAttribute(AttributeCode attribute, int value)
        {
            if (CurrentLevel < 10)
            {
                ForgingAttributes[CurrentLevel + 1] = new KeyValuePair<AttributeCode, int>(attribute, value);
            }
        }

        public void DegradeForgingAttribute()
        {
            if (CurrentLevel > 0)
            {
                ForgingAttributes.Remove(CurrentLevel);
            }
        }

        public void UpgradeCurrentLevel()
        {
            if (CurrentLevel < MaxLevel)
            {
                CurrentLevel++;
            }
        }

        public void DegradeCurrentLevel()
        {
            if (CurrentLevel > 0)
            {
                CurrentLevel--;
            }
        }
    }
}
