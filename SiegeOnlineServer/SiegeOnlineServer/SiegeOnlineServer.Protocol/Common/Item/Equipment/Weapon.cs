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

        #region 其余武器固有属性

        public int FixedAttackSpeed { get; protected set; }                                     // 武器攻击速度

        public int FixedAttackDistance { get; protected set; }                                  // 武器攻击射程

        #endregion

        public Dictionary<int, KeyValuePair<AttributeCode, float>> ForgingAttributes;           // 锻造附加属性

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

            ForgingAttributes = new Dictionary<int, KeyValuePair<AttributeCode, float>>
            {
                {-1, new KeyValuePair<AttributeCode, float>(AttributeCode.Null, 0)}
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
            ForgingAttributes = new Dictionary<int, KeyValuePair<AttributeCode, float>>
            {
                {-1, new KeyValuePair<AttributeCode, float>(AttributeCode.Null, 0)}
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

        public void UpdateForgingAttribute(int level, AttributeCode attribute, float value)
        {
            if (ForgingAttributes.ContainsKey(level))
            {
                ForgingAttributes[level] = new KeyValuePair<AttributeCode, float>(attribute, value);
            }
        }

        public void UpgradeForgingAttribute(AttributeCode attribute, float value)
        {
            if (CurrentLevel < 10)
            {
                ForgingAttributes[CurrentLevel + 1] = new KeyValuePair<AttributeCode, float>(attribute, value);
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
            if (CurrentLevel < DataConstraint.EquipmentMaxLevel)
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
        public override void Apply(Character.Character character)
        {
            base.Apply(character);
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
        }

        public override void Cancel(Character.Character character)
        {
            base.Cancel(character);
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
        }
    }
}
