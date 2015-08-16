//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Weapon.cs
//
// 文件功能描述：
//
// 武器属性
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
    /// 名称：Weapon
    /// 作者：taixihuase
    /// 作用：武器类
    /// 编写日期：2015/7/23
    /// </summary>
    [Serializable]
    public class Weapon : Equipment, IEquipment
    {
        #region 武器类型

        [Serializable]
        public enum WeaponType : byte
        {
            Null,
        }

        public byte Type { get; protected set; }

        #endregion

        #region 攻击属性类型

        [Serializable]
        public enum WeaponAttributeType : byte
        {
            Both,
            Magic,
            Physical
        }

        public WeaponAttributeType WeaponAttribute { get; protected set; }

        public Dictionary<WeaponAttributeType, KeyValuePair<int, int>> AttackLimit;             // 攻击力上下限

        #endregion

        public Dictionary<int, KeyValuePair<AttributeCode, int>> ForgingAttributes;             // 锻造附加属性

        public Weapon(Equipment equipment, WeaponType type, WeaponAttributeType weaponAttributeType)
            : base(equipment)
        {
            Type = (byte) type;
            WeaponAttribute = weaponAttributeType;
  
            if (weaponAttributeType == WeaponAttributeType.Both)
            {
                AttackLimit = new Dictionary<WeaponAttributeType, KeyValuePair<int, int>>
                {
                    [WeaponAttributeType.Magic] = new KeyValuePair<int, int>(0, 0),
                    [WeaponAttributeType.Physical] = new KeyValuePair<int, int>(0, 0)
                };
            }
            else
            {
                AttackLimit = new Dictionary<WeaponAttributeType, KeyValuePair<int, int>>
                {
                    [weaponAttributeType] = new KeyValuePair<int, int>(0, 0)
                };
            }

            ForgingAttributes = new Dictionary<int, KeyValuePair<AttributeCode, int>>
            {
                {-1, new KeyValuePair<AttributeCode, int>(AttributeCode.Null, 0)}
            };
        }

        public Weapon()
        {
            Type = (byte) WeaponType.Null;
            WeaponAttribute = WeaponAttributeType.Both;
            AttackLimit = new Dictionary<WeaponAttributeType, KeyValuePair<int, int>>
            {
                [WeaponAttributeType.Magic] = new KeyValuePair<int, int>(0, 0),
                [WeaponAttributeType.Physical] = new KeyValuePair<int, int>(0, 0)
            };
            ForgingAttributes = new Dictionary<int, KeyValuePair<AttributeCode, int>>
            {
                {-1, new KeyValuePair<AttributeCode, int>(AttributeCode.Null, 0)}
            };
        }

        public void UpdateAttackLimit(int? physicalMin, int? physicalMax, int? magicMin, int? magicMax)
        {
            if (physicalMin.HasValue && physicalMax.HasValue)
            {
                AttackLimit[WeaponAttributeType.Physical] = new KeyValuePair<int, int>(physicalMin.Value,
                    physicalMax.Value);
            }
            if (magicMin.HasValue && magicMax.HasValue)
            {
                AttackLimit[WeaponAttributeType.Magic] = new KeyValuePair<int, int>(magicMin.Value,
                    magicMax.Value);
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
