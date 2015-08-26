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
using System.Globalization;
using System.IO;

// ReSharper disable AssignNullToNotNullAttribute
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

        public WeaponType Type { get; protected set; } // 武器类型

        #endregion

        #region 攻击属性类型

        /// <summary>
        /// 类型：枚举
        /// 名称：WeaponAttackType
        /// 作者：taixihuase
        /// 作用：武器攻击属性类型枚举
        /// 编写日期：2015/8/16
        /// </summary>
        [Serializable]
        public enum WeaponAttackType : byte
        {
            Both,
            Magic,
            Physical
        }

        public WeaponAttackType WeaponAttack { get; protected set; } // 武器攻击属性类型

        public Dictionary<WeaponAttackType, KeyValuePair<int, int>> AttackLimit; // 攻击力上下限

        #endregion

        #region 元素属性类型

        /// <summary>
        /// 类型：枚举
        /// 名称：WeaponElementType
        /// 作者：taixihuase
        /// 作用：武器元素属性类型枚举
        /// 编写日期：2015/8/22
        /// </summary>
        [Serializable]
        public enum WeaponElementType : byte
        {
            Null,
            Fire,
            Frost,
            Lightning,
            Poison,
            Rock,
            Shadow,
            Storm
        }

        /// <summary>
        /// 类型：枚举
        /// 名称：ElementExtraAttribute
        /// 作者：taixihuase
        /// 作用：武器元素附加属性类型
        /// 编写日期：2015/8/22
        /// </summary>
        [Serializable]
        protected enum ElementExtraAttribute : byte
        {
            Null,

            Attack_Percent_Both,
            Life_Increase_Percent,
            Crit_Both,
            Life_Steal,
            Defense_Percent_Both,
            Dodge_Both,
            Hit_Both,

            Penetration,
            Immunity,
            Speed_Attack,
            Feedback,
            Rebound,
            Speed_Cooldown,
            Speed_Movement
        }

        public WeaponElementType WeaponElement { get; set; } // 武器元素属性类型

        public int CurrentElementEnergy { get; set; } // 当前元素能量值

        public int MaxElementEnergy { get; protected set; } // 最大元素能量值
 
        public KeyValuePair<WeaponElementType, Dictionary<AttributeCode, float>> ElementAttributes; // 元素附加属性 

        #endregion

        #region 其余武器固有属性

        public float FixedAttackSpeed { get; protected set; } // 武器攻击速度

        public float FixedAttackDistance { get; protected set; } // 武器攻击射程

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
        /// <param name="upgrade"></param>
        /// <param name="cur"></param>
        /// <param name="dur"></param>
        /// <param name="elem"></param>
        /// <param name="distance"></param>
        /// <param name="type"></param>
        /// <param name="weaponAttackType"></param>
        /// <param name="weaponElement"></param>
        /// <param name="speed"></param>
        public Weapon(int fixedId, int allocatedId, string name, OccupationCode occupation, int limit, bool upgrade,
            int cur, int dur, int elem, float speed, float distance, 
            WeaponType type, WeaponAttackType weaponAttackType, WeaponElementType weaponElement)
            : base(fixedId, allocatedId, name, occupation, limit, upgrade, cur, dur, EquipmentType.Weapon)
        {
            Type = type;

            #region 武器属性初始化

            WeaponAttack = weaponAttackType;
            if (weaponAttackType == WeaponAttackType.Both)
            {
                AttackLimit =
                    new Dictionary<WeaponAttackType, KeyValuePair<int, int>>(new EnumComparer<WeaponAttackType>())
                    {
                        [WeaponAttackType.Magic] = new KeyValuePair<int, int>(0, 0),
                        [WeaponAttackType.Physical] = new KeyValuePair<int, int>(0, 0)
                    };
            }
            else
            {
                AttackLimit =
                    new Dictionary<WeaponAttackType, KeyValuePair<int, int>>(new EnumComparer<WeaponAttackType>())
                    {
                        [weaponAttackType] = new KeyValuePair<int, int>(0, 0)
                    };
            }

            #endregion

            ForgingAttributes = new Dictionary<int, KeyValuePair<AttributeCode, float>>
            {
                {-1, new KeyValuePair<AttributeCode, float>(AttributeCode.Null, 0)}
            };
            FixedAttackSpeed = speed;
            FixedAttackDistance = distance;

            #region 武器元素初始化

            WeaponElement = weaponElement;
            CurrentElementEnergy = elem;
            if (weaponElement != WeaponElementType.Null)
            {
                ElementAttributes =
                    new KeyValuePair<WeaponElementType, Dictionary<AttributeCode, float>>(weaponElement,
                        new Dictionary<AttributeCode, float>(new EnumComparer<AttributeCode>())
                        {
                            {(AttributeCode) Enum.Parse(typeof (AttributeCode), "Enhance_" + weaponElement), 0}
                        });

                int num = Convert.ToInt32(Enum.Parse(typeof (WeaponElementType), weaponElement.ToString()));
                int mul = 0;
                while ((num += mul*DataConstraint.ElementMaxSize) <=
                       Enum.GetValues(typeof (ElementExtraAttribute)).Length)
                {
                    var s = Enum.GetName(typeof (ElementExtraAttribute), num);
                    if (s != null)
                    {
                        ElementAttributes.Value.Add((AttributeCode) Enum.Parse(typeof (AttributeCode), s), 0);
                    }
                    mul++;
                }
            }
            else
            {
                ElementAttributes =
                    new KeyValuePair<WeaponElementType, Dictionary<AttributeCode, float>>(WeaponElementType.Null,
                        new Dictionary<AttributeCode, float>(new EnumComparer<AttributeCode>())
                        {
                            {AttributeCode.Null, 0}
                        });
            }

            #endregion
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
            Type = WeaponType.Null;
            WeaponAttack = WeaponAttackType.Both;
            AttackLimit = new Dictionary<WeaponAttackType, KeyValuePair<int, int>>(new EnumComparer<WeaponAttackType>())
            {
                {WeaponAttackType.Both, new KeyValuePair<int, int>(0, 0)}
            };
            WeaponElement = WeaponElementType.Null;
            ElementAttributes =
                new KeyValuePair<WeaponElementType, Dictionary<AttributeCode, float>>(WeaponElementType.Null,
                    new Dictionary<AttributeCode, float>(new EnumComparer<AttributeCode>())
                    {
                        {AttributeCode.Null, 0}
                    });
            ForgingAttributes = new Dictionary<int, KeyValuePair<AttributeCode, float>>
            {
                {-1, new KeyValuePair<AttributeCode, float>(AttributeCode.Null, 0)}
            };
            FixedAttackSpeed = DataConstraint.CharacterDefaultAttackSpeed;
            FixedAttackDistance = DataConstraint.CharacterDefaultAttackDistance;
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
                if (AttackLimit.ContainsKey(WeaponAttackType.Physical))
                {
                    AttackLimit[WeaponAttackType.Physical] = new KeyValuePair<int, int>(physicalMin.Value,
                        physicalMax.Value);
                }
            }
            if (magicMin.HasValue && magicMax.HasValue)
            {
                if (AttackLimit.ContainsKey(WeaponAttackType.Magic))
                {
                    AttackLimit[WeaponAttackType.Magic] = new KeyValuePair<int, int>(magicMin.Value,
                        magicMax.Value);
                }
            }
        }


        #region 元素属性操作相关方法

        public void ClearElementAttribute()
        {
            WeaponElement = WeaponElementType.Null;
            CurrentElementEnergy = 0;
            ElementAttributes =
                new KeyValuePair<WeaponElementType, Dictionary<AttributeCode, float>>(WeaponElementType.Null,
                    new Dictionary<AttributeCode, float>(new EnumComparer<AttributeCode>())
                    {
                        {AttributeCode.Null, 0}
                    });
        }

        public void UpgradeElementAttribute(int value)
        {
            if (WeaponElement != WeaponElementType.Null)
            {
                if (CurrentElementEnergy < DataConstraint.WeaponMaxElementEnergy)
                {
                    UpgradeElementEnergy(value);
                }
            }
        }

        private void UpgradeElementEnergy(int value)
        {
            CurrentElementEnergy += value;
            if (CurrentElementEnergy >= DataConstraint.WeaponMaxElementEnergy)
            {
                CurrentElementEnergy = DataConstraint.WeaponMaxElementEnergy;
            }
        }

        public void UpdateElementAttribute(WeaponElementType weaponElement)
        {
            ClearElementAttribute();
            AddElementAttribute(weaponElement);
        }

        private void AddElementAttribute(WeaponElementType weaponElement)
        {
            if (weaponElement != WeaponElementType.Null)
            {
                WeaponElement = weaponElement;
                ElementAttributes =
                    new KeyValuePair<WeaponElementType, Dictionary<AttributeCode, float>>(weaponElement,
                        new Dictionary<AttributeCode, float>(new EnumComparer<AttributeCode>())
                        {
                            {(AttributeCode) Enum.Parse(typeof (AttributeCode), "Enhance_" + weaponElement), 0}
                        });

                int num = Convert.ToInt32(Enum.Parse(typeof(WeaponElementType), weaponElement.ToString()));
                int mul = 0;
                while ((num += mul * DataConstraint.ElementMaxSize) <=
                       Enum.GetValues(typeof(ElementExtraAttribute)).Length)
                {
                    var s = Enum.GetName(typeof(ElementExtraAttribute), num);
                    if (s != null)
                    {
                        ElementAttributes.Value.Add((AttributeCode)Enum.Parse(typeof(AttributeCode), s), 0);
                    }
                    mul++;
                }
            }
        }

        public void UpgradeElementEnhanceAttribute(int value)
        {
            if (WeaponElement != WeaponElementType.Null)
            {
                AttributeCode enhance = (AttributeCode)Enum.Parse(typeof(AttributeCode), "Enhance_" + WeaponElement);
                ElementAttributes.Value[enhance] = ElementAttributes.Value[enhance] + value;
            }
        }

        public void UpgradeElementExtraAttribute(float value, int order)
        {
            if (WeaponElement != WeaponElementType.Null)
            {
                int num = Convert.ToInt32(Enum.Parse(typeof (WeaponElementType), WeaponElement.ToString())) +
                          (order - 1)*DataConstraint.ElementMaxSize;
                if (num > 0 && num <= Enum.GetValues(typeof(ElementExtraAttribute)).Length)
                {
                    string extra = Enum.GetName(typeof(ElementExtraAttribute), num);
                    AttributeCode attr = (AttributeCode)Enum.Parse(typeof(AttributeCode), extra);
                    ElementAttributes.Value[attr] = ElementAttributes.Value[attr] + value;     
                }
            }
        }

        #endregion

        #region IEquipment接口实现

        public void Upgrade(AttributeCode attribute = AttributeCode.Null, ValueType value = null)
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

        protected override void UpdateForgingAttribute(int level, AttributeCode attribute, ValueType value)
        {
            if (ForgingAttributes.ContainsKey(level) && !attribute.Equals(AttributeCode.Null) &&
                value != null)
            {
                float f = Convert.ToSingle(value.ToString());
                if (f > 0)
                {
                    ForgingAttributes[level] = new KeyValuePair<AttributeCode, float>(attribute, f);
                }
            }
        }

        protected override void UpgradeForgingAttribute(AttributeCode attribute, ValueType value)
        {
            if (CurrentLevel < DataConstraint.EquipmentMaxLevel && !attribute.Equals(AttributeCode.Null) && value != null)
            {
                float f = Convert.ToSingle(value.ToString());
                if (f > 0)
                {
                    ForgingAttributes[CurrentLevel + 1] = new KeyValuePair<AttributeCode, float>(attribute, f);
                }
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
                character.Attribute.WeaponExtraAttackSpeed = FixedAttackSpeed -
                                                             DataConstraint.CharacterDefaultAttackSpeed;
                character.Attribute.WeaponExtraAttackDistance = FixedAttackDistance -
                                                                DataConstraint.CharacterDefaultAttackDistance;
               
                if (AttackLimit.ContainsKey(WeaponAttackType.Physical))
                {
                    character.Attribute.AttackPhysicalMin += AttackLimit[WeaponAttackType.Physical].Key;
                    character.Attribute.AttackPhysicalMax += AttackLimit[WeaponAttackType.Physical].Value;
                }
                if (AttackLimit.ContainsKey(WeaponAttackType.Magic))
                {
                    character.Attribute.AttackMagicMin += AttackLimit[WeaponAttackType.Magic].Key;
                    character.Attribute.AttackMagicMax += AttackLimit[WeaponAttackType.Magic].Value;
                }
                foreach (KeyValuePair<int, KeyValuePair<AttributeCode, float>> attribute in ForgingAttributes)
                {
                    if (attribute.Key > 0)
                    {
                        UpdateCharacterAttribute(character, attribute.Value, true);
                    }
                }
                foreach (KeyValuePair<AttributeCode, float> attribute in ElementAttributes.Value)
                {
                    UpdateCharacterAttribute(character, attribute, true);
                }
                character.Attribute.CalculateAttributes();
                return true;
            }
            return false;
        }

        public override bool Cancel(Character.Character character)
        {
            if (base.Cancel(character))
            {
                character.Attribute.WeaponExtraAttackSpeed = 0;
                character.Attribute.WeaponExtraAttackDistance = 0;
                if (AttackLimit.ContainsKey(WeaponAttackType.Physical))
                {
                    character.Attribute.AttackPhysicalMin -= AttackLimit[WeaponAttackType.Physical].Key;
                    character.Attribute.AttackPhysicalMax -= AttackLimit[WeaponAttackType.Physical].Value;
                }
                if (AttackLimit.ContainsKey(WeaponAttackType.Magic))
                {
                    character.Attribute.AttackMagicMin -= AttackLimit[WeaponAttackType.Magic].Key;
                    character.Attribute.AttackMagicMax -= AttackLimit[WeaponAttackType.Magic].Value;
                }
                foreach (KeyValuePair<int, KeyValuePair<AttributeCode, float>> attribute in ForgingAttributes)
                {
                    if (attribute.Key > 0)
                    {
                        UpdateCharacterAttribute(character, attribute.Value, true);
                    }
                }
                foreach (KeyValuePair<AttributeCode, float> attribute in ElementAttributes.Value)
                {
                    UpdateCharacterAttribute(character, attribute, false);
                }
                character.Attribute.CalculateAttributes();
                return true;
            }
            return false;
        }

        #endregion
    }
}
