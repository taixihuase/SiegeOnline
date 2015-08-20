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

        /// <summary>
        /// 类型：枚举
        /// 名称：ArmorType
        /// 作者：taixihuase
        /// 作用：防具类型枚举
        /// 编写日期：2015/8/16
        /// </summary>
        [Serializable]
        public enum ArmorType : byte
        {
            Null,
            Armor,
            Boots,
            Gloves,
            Helmet,
        }

        public byte Type { get; protected set; } // 防具类型

        #endregion

        public int[] DefensePoints { get; set; } // 双防加成数值

        public Dictionary<int, KeyValuePair<AttributeCode, float>> ForgingAttributes; // 锻造附加属性

        /// <summary>
        /// 类型：方法
        /// 名称：Armor
        /// 作者：taixihuase
        /// 作用：通过数据库中获得的数据构造防具装备实例
        /// 编写日期：2015/8/16
        /// </summary>
        /// <param name="fixedId"></param>
        /// <param name="allocatedId"></param>
        /// <param name="name"></param>
        /// <param name="occupation"></param>
        /// <param name="limit"></param>
        /// <param name="cur"></param>
        /// <param name="dur"></param>
        /// <param name="type"></param>
        public Armor(int fixedId, int allocatedId, string name, OccupationCode occupation, int limit, int cur, int dur,
            ArmorType type)
            : base(fixedId, allocatedId, name, occupation, limit, cur, dur, EquipmentType.Armor)
        {
            Type = (byte) type;
            DefensePoints = new[] {0, 0};
            ForgingAttributes = new Dictionary<int, KeyValuePair<AttributeCode, float>>
            {
                {-1, new KeyValuePair<AttributeCode, float>(AttributeCode.Null, 0)}
            };
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Armor
        /// 作者：taixihuase
        /// 作用：构造空的防具装备实例
        /// 编写日期：2015/8/16
        /// </summary>
        public Armor()
        {
            DefensePoints = new[] {0, 0};
            ForgingAttributes = new Dictionary<int, KeyValuePair<AttributeCode, float>>
            {
                {-1, new KeyValuePair<AttributeCode, float>(AttributeCode.Null, 0)}
            };
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UpdateDefensePoints
        /// 作者：taixihuase
        /// 作用：变更防具的双防数值
        /// 编写日期：2015/8/16
        /// </summary>
        /// <param name="physical"></param>
        /// <param name="magic"></param>
        public void UpdateDefensePoints(int physical, int magic)
        {
            if (DefensePoints.Count() >= 0)
            {
                DefensePoints[0] = physical;
                DefensePoints[1] = magic;
            }
        }

        #region IEquipment接口实现

        public void Upgrade(AttributeCode attribute = AttributeCode.Null, float value = 0)
        {
            UpgradeForgingAttribute(attribute, value);
            UpgradeCurrentLevel();
        }

        public void Degrade()
        {
            DegradeForgingAttribute();
            DegradeCurrentLevel();
        }

        #endregion

        #region 重载抽象基类方法

        protected override void UpdateForgingAttribute(int level, AttributeCode attribute, float value)
        {
            if (ForgingAttributes.ContainsKey(level) && !attribute.Equals(AttributeCode.Null) && Math.Abs(value) > 0)
            {
                ForgingAttributes[level] = new KeyValuePair<AttributeCode, float>(attribute, value);
            }
        }

        protected override void UpgradeForgingAttribute(AttributeCode attribute, float value)
        {
            if (CurrentLevel < DataConstraint.EquipmentMaxLevel && !attribute.Equals(AttributeCode.Null) &&
                Math.Abs(value) > 0)
            {
                ForgingAttributes[CurrentLevel + 1] = new KeyValuePair<AttributeCode, float>(attribute, value);
            }
        }

        protected override void DegradeForgingAttribute()
        {
            if (CurrentLevel > 0 && ForgingAttributes.ContainsKey(CurrentLevel))
            {
                ForgingAttributes.Remove(CurrentLevel);
            }
        }

        public override bool Apply(Character.Character character)
        {
            if (base.Apply(character))
            {
                character.Attribute.DefensePhysical[1] += DefensePoints[0];
                character.Attribute.DefenseMagic[1] += DefensePoints[1];
                foreach (KeyValuePair<int, KeyValuePair<AttributeCode, float>> attribute in ForgingAttributes)
                {
                    if (attribute.Key > 0)
                    {
                        UpdateCharacterAttribute(character, attribute.Value, true);
                    }
                }
                CalculateCharacterAttributes(character);
                return true;
            }
            return false;
        }

        public override bool Cancel(Character.Character character)
        {
            if (base.Cancel(character))
            {
                character.Attribute.DefensePhysical[1] -= DefensePoints[0];
                character.Attribute.DefenseMagic[1] -= DefensePoints[1];
                foreach (KeyValuePair<int, KeyValuePair<AttributeCode, float>> attribute in ForgingAttributes)
                {
                    if (attribute.Key > 0)
                    {
                        UpdateCharacterAttribute(character, attribute.Value, false);
                    }
                }
                CalculateCharacterAttributes(character);
                return true;
            }
            return false;
        }

        #endregion
    }
}
