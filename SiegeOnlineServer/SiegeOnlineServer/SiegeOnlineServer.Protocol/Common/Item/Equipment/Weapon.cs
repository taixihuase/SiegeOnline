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

        /// <summary>
        /// 类型：枚举
        /// 名称：WeaponType
        /// 作者：taixihuase
        /// 作用：武器类型枚举
        /// 编写日期：2015/8/16
        /// </summary>
        [Serializable]
        public enum WeaponType : byte
        {
            Null,
        }

        public byte Type { get; protected set; } // 武器类型

        #endregion

        #region 攻击属性类型

        /// <summary>
        /// 类型：枚举
        /// 名称：WeaponAttributeType
        /// 作者：taixihuase
        /// 作用：武器攻击属性类型枚举
        /// 编写日期：2015/8/16
        /// </summary>
        [Serializable]
        public enum WeaponAttributeType : byte
        {
            Both,
            Magic,
            Physical
        }

        public WeaponAttributeType WeaponAttribute { get; protected set; } // 武器攻击属性类型

        public Dictionary<WeaponAttributeType, KeyValuePair<int, int>> AttackLimit; // 攻击力上下限

        #endregion

        #region 其余武器固有属性

        public int FixedAttackSpeed { get; protected set; } // 武器攻击速度

        public int FixedAttackDistance { get; protected set; } // 武器攻击射程

        #endregion

        public Dictionary<int, KeyValuePair<AttributeCode, float>> ForgingAttributes; // 锻造附加属性

        /// <summary>
        /// 类型：方法
        /// 名称：Weapon
        /// 作者：taixihuase
        /// 作用：通过数据库中获得的数据构造武器装备实例
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
        /// <param name="weaponAttributeType"></param>
        public Weapon(int fixedId, int allocatedId, string name, OccupationCode occupation, int limit, int cur, int dur,
            WeaponType type, WeaponAttributeType weaponAttributeType)
            : base(fixedId, allocatedId, name, occupation, limit, cur, dur, EquipmentType.Weapon)
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

            ForgingAttributes = new Dictionary<int, KeyValuePair<AttributeCode, float>>
            {
                {-1, new KeyValuePair<AttributeCode, float>(AttributeCode.Null, 0)}
            };
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Weapon
        /// 作者：taixihuase
        /// 作用：构造空的武器装备实例
        /// 编写日期：2015/8/16
        /// </summary>
        public Weapon()
        {
            Type = (byte) WeaponType.Null;
            WeaponAttribute = WeaponAttributeType.Both;
            AttackLimit = new Dictionary<WeaponAttributeType, KeyValuePair<int, int>>
            {
                [WeaponAttributeType.Magic] = new KeyValuePair<int, int>(0, 0),
                [WeaponAttributeType.Physical] = new KeyValuePair<int, int>(0, 0)
            };
            ForgingAttributes = new Dictionary<int, KeyValuePair<AttributeCode, float>>
            {
                {-1, new KeyValuePair<AttributeCode, float>(AttributeCode.Null, 0)}
            };
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UpdateAttackLimit
        /// 作者：taixihuase
        /// 作用：变更武器攻击上下限
        /// 编写日期：2015/8/16
        /// </summary>
        /// <param name="physicalMin"></param>
        /// <param name="physicalMax"></param>
        /// <param name="magicMin"></param>
        /// <param name="magicMax"></param>
        public void UpdateAttackLimit(int? physicalMin, int? physicalMax, int? magicMin, int? magicMax)
        {
            if (physicalMin.HasValue && physicalMax.HasValue)
            {
                if (AttackLimit.ContainsKey(WeaponAttributeType.Physical))
                {
                    AttackLimit[WeaponAttributeType.Physical] = new KeyValuePair<int, int>(physicalMin.Value,
                        physicalMax.Value);
                }
            }
            if (magicMin.HasValue && magicMax.HasValue)
            {
                if (AttackLimit.ContainsKey(WeaponAttributeType.Magic))
                {
                    AttackLimit[WeaponAttributeType.Magic] = new KeyValuePair<int, int>(magicMin.Value,
                        magicMax.Value);
                }
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
                character.Attribute.AttackSpeed = FixedAttackSpeed;
                character.Attribute.AttackDistance = FixedAttackDistance;
                if (AttackLimit.ContainsKey(WeaponAttributeType.Physical))
                {
                    character.Attribute.AttackPhysical[1] += AttackLimit[WeaponAttributeType.Physical].Key;
                    character.Attribute.AttackPhysical[2] += AttackLimit[WeaponAttributeType.Physical].Value;
                }
                if (AttackLimit.ContainsKey(WeaponAttributeType.Magic))
                {
                    character.Attribute.AttackMagic[1] += AttackLimit[WeaponAttributeType.Magic].Key;
                    character.Attribute.AttackMagic[2] += AttackLimit[WeaponAttributeType.Magic].Value;
                }
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
                character.Attribute.AttackSpeed = DataConstraint.CharacterDefaultAttackSpeed;
                character.Attribute.AttackDistance = DataConstraint.CharacterDefaultAttackDistance;
                if (AttackLimit.ContainsKey(WeaponAttributeType.Physical))
                {
                    character.Attribute.AttackPhysical[1] -= AttackLimit[WeaponAttributeType.Physical].Key;
                    character.Attribute.AttackPhysical[2] -= AttackLimit[WeaponAttributeType.Physical].Value;
                }
                if (AttackLimit.ContainsKey(WeaponAttributeType.Magic))
                {
                    character.Attribute.AttackMagic[1] -= AttackLimit[WeaponAttributeType.Magic].Key;
                    character.Attribute.AttackMagic[2] -= AttackLimit[WeaponAttributeType.Magic].Value;
                }
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
