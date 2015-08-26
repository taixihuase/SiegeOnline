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
using System.Linq;
using SiegeOnlineServer.Protocol.Common.Character;

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
    public abstract class Equipment : IItem
    {
        public int FixedId { get; protected set; } // 数据库固定编号

        public int AllocatedId { get; protected set; } // 动态分配编号

        public string Name { get; protected set; } // 名称

        public byte Occupation { get; protected set; } // 职业需求

        public int LevelLimit { get; protected set; } // 等级需求

        public int CurrentLevel { get; set; } // 装备锻造等级

        public int Durability { get; set; } // 耐久度

        public bool IsCanUpgrade { get; protected set; } // 是否可升级

        public bool IsUsing { get; set; } // 是否正在穿戴

        #region 装备类型

        /// <summary>
        /// 类型：枚举
        /// 名称：EquipmentType
        /// 作者：taixihuase
        /// 作用：装备类型枚举
        /// 编写日期：2015/8/16
        /// </summary>
        [Serializable]
        public enum EquipmentType : byte
        {
            Null,
            Armor,
            Jewel,
            Weapon
        }

        public EquipmentType EquipType { get; protected set; } // 装备类型

        #endregion

        public Dictionary<AttributeCode, float> FixedAttributes; // 固定属性（多条）
        public KeyValuePair<AttributeCode, float> RandomAttribute; // 随机属性（单条）

        /// <summary>
        /// 类型：方法
        /// 名称：Equipment
        /// 作者：taixihuase
        /// 作用：通过数据库中获得的数据构造实例，只能由子类调用
        /// 编写日期：2015/8/16
        /// </summary>
        /// <param name="fixed"></param>
        /// <param name="allocated"></param>
        /// <param name="name"></param>
        /// <param name="occupation"></param>
        /// <param name="limit"></param>
        /// <param name="upgrade"></param>
        /// <param name="cur"></param>
        /// <param name="durability"></param>
        /// <param name="type"></param>
        protected Equipment(int @fixed, int allocated, string name, OccupationCode occupation, int limit, bool upgrade,
            int cur, int durability, EquipmentType type)
        {
            FixedId = @fixed;
            AllocatedId = allocated;
            Name = name;
            Occupation = (byte) occupation;
            LevelLimit = limit;
            IsCanUpgrade = upgrade;
            CurrentLevel = cur;
            Durability = durability;
            IsUsing = false;
            EquipType = type;
            FixedAttributes = new Dictionary<AttributeCode, float>(new EnumComparer<AttributeCode>())
            {
                {AttributeCode.Null, 0}
            };
            RandomAttribute = new KeyValuePair<AttributeCode, float>(AttributeCode.Null, 0);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Equipment
        /// 作者：taixihuase
        /// 作用：构造一个空的实例，只能由子类调用
        /// 编写日期：2015/8/16
        /// </summary>
        protected Equipment()
        {
            FixedId = 0;
            AllocatedId = 0;
            Name = "";
            Occupation = (byte) OccupationCode.Common;
            LevelLimit = 0;
            IsCanUpgrade = false;
            CurrentLevel = 0;
            Durability = DataConstraint.EquipmentMaxDurability;
            IsUsing = false;
            EquipType = EquipmentType.Null;
            FixedAttributes = new Dictionary<AttributeCode, float>(new EnumComparer<AttributeCode>())
            {
                {AttributeCode.Null, 0}
            };
            RandomAttribute = new KeyValuePair<AttributeCode, float>(AttributeCode.Null, 0);
        }

        #region IItem接口实现

        public virtual bool Apply(Character.Character character)
        {
            if (!IsUsing)
            {
                foreach (KeyValuePair<AttributeCode, float> attribute in FixedAttributes)
                {
                    UpdateCharacterAttribute(character, attribute, true);
                }
                UpdateCharacterAttribute(character, RandomAttribute, true);
                character.Attribute.CalculateAttributes();
                IsUsing = true;
                return true;
            }
            return false;
        }

        public virtual bool Cancel(Character.Character character)
        {
            if (IsUsing)
            {
                foreach (KeyValuePair<AttributeCode, float> attribute in FixedAttributes)
                {
                    UpdateCharacterAttribute(character, attribute, false);
                }
                UpdateCharacterAttribute(character, RandomAttribute, false);
                character.Attribute.CalculateAttributes();
                IsUsing = false;
                return true;
            }
            return false;
        }

        #endregion

        #region 装备通用接口

        /// <summary>
        /// 类型：方法
        /// 名称：RemoveFixedAttribute
        /// 作者：taixihuase
        /// 作用：移除一条固定属性
        /// 编写日期：2015/8/20
        /// </summary>
        /// <param name="attribute"></param>
        public void RemoveFixedAttribute(AttributeCode attribute)
        {
            FixedAttributes.Remove(attribute);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UpdateFixedAttribute
        /// 作者：taixihuase
        /// 作用：变更一条固定属性，含添加、更改及删除操作
        /// 编写日期：2015/8/20
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        public void UpdateFixedAttribute(AttributeCode attribute, float value)
        {
            if (Math.Abs(value) > 0)
            {
                if (FixedAttributes.ContainsKey(attribute))
                {
                    FixedAttributes[attribute] = value;
                }
                else
                {
                    FixedAttributes.Add(attribute, value);
                }
            }
            else
            {
                RemoveFixedAttribute(attribute);
            }
        }


        /// <summary>
        /// 类型：方法
        /// 名称：RemoveRandomAttribute
        /// 作者：taixihuase
        /// 作用：移除随机属性
        /// 编写日期：2015/8/20
        /// </summary>
        public void RemoveRandomAttribute()
        {
            RandomAttribute = new KeyValuePair<AttributeCode, float>(AttributeCode.Null, 0);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UpdateRandomAttribute
        /// 作者：taixihuase
        /// 作用：变更随机属性，含添加、更改及删除操作
        /// 编写日期：2015/8/20
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        public void UpdateRandomAttribute(AttributeCode attribute, float value)
        {
            if (attribute != AttributeCode.Null)
            {
                if (value > 0)
                {
                    RandomAttribute = new KeyValuePair<AttributeCode, float>(attribute, value);
                }
            }
            else
            {
                RemoveRandomAttribute();
            }
        }


        /// <summary>
        /// 类型：方法
        /// 名称：Repair
        /// 作者：taixihuase
        /// 作用：修复装备的耐久度至最大值
        /// 编写日期：2015/8/19
        /// </summary>
        public void Repair()
        {
            if (Durability < DataConstraint.EquipmentMaxDurability)
            {
                Durability = DataConstraint.EquipmentMaxDurability;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Abrase
        /// 作者：taixihuase
        /// 作用：使装备遭受磨损，降低耐久度
        /// 编写日期：2015/8/19
        /// </summary>
        /// <param name="value"></param>
        public void Abrase(int value)
        {
            Durability -= value;
            if (Durability <= 0)
            {
                Durability = 0;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UpdateCharacterAttribute
        /// 作者：taixihuase
        /// 作用：更新一条装备属性对角色属性的作用效果
        /// 编写日期：2015/8/19
        /// </summary>
        /// <param name="character"></param>
        /// <param name="attribute"></param>
        /// <param name="activated"></param>
        protected void UpdateCharacterAttribute(Character.Character character, KeyValuePair<AttributeCode, float> attribute, bool activated)
        {
            int sign = activated ? 1 : -1;

            float f = attribute.Value;
            int i = Convert.ToInt32(f);

            switch (attribute.Key)
            {
                case AttributeCode.Null:

                    break;

                    #region 特殊命名格式的属性值改变

                case AttributeCode.Attack_Both:
                    character.Attribute.AttackPhysicalMin += i*sign;
                    character.Attribute.AttackPhysicalMax += i*sign;
                    if (character.Attribute.AttackPhysicalMin <= 0)
                    {
                        character.Attribute.AttackPhysicalMin = 0;
                    }
                    if (character.Attribute.AttackPhysicalMax <= 0)
                    {
                        character.Attribute.AttackPhysicalMax = 0;
                    }
                    character.Attribute.AttackMagicMin += i*sign;
                    character.Attribute.AttackMagicMax += i*sign;
                    if (character.Attribute.AttackMagicMin <= 0)
                    {
                        character.Attribute.AttackMagicMin = 0;
                    }
                    if (character.Attribute.AttackMagicMax <= 0)
                    {
                        character.Attribute.AttackMagicMax = 0;
                    }
                    break;

                case AttributeCode.Attack_Magic:
                    character.Attribute.AttackMagicMin += i*sign;
                    character.Attribute.AttackMagicMax += i*sign;
                    if (character.Attribute.AttackMagicMin <= 0)
                    {
                        character.Attribute.AttackMagicMin = 0;
                    }
                    if (character.Attribute.AttackMagicMax <= 0)
                    {
                        character.Attribute.AttackMagicMax = 0;
                    }
                    break;

                case AttributeCode.Attack_Physical:
                    character.Attribute.AttackPhysicalMin += i*sign;
                    character.Attribute.AttackPhysicalMax += i*sign;
                    if (character.Attribute.AttackPhysicalMin <= 0)
                    {
                        character.Attribute.AttackPhysicalMin = 0;
                    }
                    if (character.Attribute.AttackPhysicalMax <= 0)
                    {
                        character.Attribute.AttackPhysicalMax = 0;
                    }
                    break;

                case AttributeCode.Attack_Percent_Both:
                    character.Attribute.AttackPercentPhysical += f*sign;
                    if (character.Attribute.AttackPercentPhysical <= 0)
                    {
                        character.Attribute.AttackPercentPhysical = 0;
                    }
                    character.Attribute.AttackPercentMagic += f*sign;
                    if (character.Attribute.AttackPercentMagic <= 0)
                    {
                        character.Attribute.AttackPercentMagic = 0;
                    }
                    break;

                case AttributeCode.Attack_Percent_Physical:
                    character.Attribute.AttackPercentPhysical += f*sign;
                    if (character.Attribute.AttackPercentPhysical <= 0)
                    {
                        character.Attribute.AttackPercentPhysical = 0;
                    }
                    break;

                case AttributeCode.Attack_Percent_Magic:
                    character.Attribute.AttackPercentMagic += f*sign;
                    if (character.Attribute.AttackPercentMagic <= 0)
                    {
                        character.Attribute.AttackPercentMagic = 0;
                    }
                    break;

                case AttributeCode.Defense_Both:
                    character.Attribute.DefensePhysicalBase += i*sign;
                    if (character.Attribute.DefensePhysicalBase <= 0)
                    {
                        character.Attribute.DefensePhysicalBase = 0;
                    }
                    character.Attribute.DefenseMagicBase += i*sign;
                    if (character.Attribute.DefenseMagicBase <= 0)
                    {
                        character.Attribute.DefenseMagicBase = 0;
                    }
                    break;

                case AttributeCode.Defense_Physical:
                    character.Attribute.DefensePhysicalBase += i*sign;
                    if (character.Attribute.DefensePhysicalBase <= 0)
                    {
                        character.Attribute.DefensePhysicalBase = 0;
                    }
                    break;

                case AttributeCode.Defense_Magic:
                    character.Attribute.DefenseMagicBase += i*sign;
                    if (character.Attribute.DefenseMagicBase <= 0)
                    {
                        character.Attribute.DefenseMagicBase = 0;
                    }
                    break;

                case AttributeCode.Defense_Percent_Both:
                    character.Attribute.DefensePercentPhysical += f*sign;
                    if (character.Attribute.DefensePercentPhysical <= 0)
                    {
                        character.Attribute.DefensePercentPhysical = 0;
                    }
                    character.Attribute.DefensePercentMagic += f*sign;
                    if (character.Attribute.DefensePercentMagic <= 0)
                    {
                        character.Attribute.DefensePercentMagic = 0;
                    }
                    break;

                case AttributeCode.Defense_Percent_Physical:
                    character.Attribute.DefensePercentPhysical += f*sign;
                    if (character.Attribute.DefensePercentPhysical <= 0)
                    {
                        character.Attribute.DefensePercentPhysical = 0;
                    }
                    break;

                case AttributeCode.Defense_Percent_Magic:
                    character.Attribute.DefensePercentMagic += f*sign;
                    if (character.Attribute.DefensePercentMagic <= 0)
                    {
                        character.Attribute.DefensePercentMagic = 0;
                    }
                    break;

                case AttributeCode.Life_Increase:
                    character.Attribute.HitPointBase += i*sign;
                    if (character.Attribute.HitPointBase <= 0)
                    {
                        character.Attribute.HitPointBase = 0;
                    }
                    break;

                case AttributeCode.Mana_Increase:
                    character.Attribute.ManaBase += i*sign;
                    if (character.Attribute.ManaBase <= 0)
                    {
                        character.Attribute.ManaBase = 0;
                    }
                    break;

                    #endregion

                    #region 根据枚举名反射改变属性值

                default:
                {
                    string code = attribute.Key.ToString().Replace("_", "");
                    Type type = typeof (CharacterAttribute);

                    if (code.Contains("Both"))
                    {
                        string physical = code.Replace("Both", "Physical");
                        var pi = type.GetProperty(physical);
                        float pv = (float) pi.GetValue(character.Attribute, null);
                        pv += f*sign;
                        if (pv <= 0)
                        {
                            pv = 0;
                        }
                        pi.SetValue(character.Attribute, pv, null);

                        string magic = code.Replace("Both", "Magic");
                        pi = type.GetProperty(magic);
                        float mv = (float) pi.GetValue(character.Attribute, null);
                        mv += f*sign;
                        if (mv <= 0)
                        {
                            mv = 0;
                        }
                        pi.SetValue(character.Attribute, mv, null);
                    }
                    else if (code.Contains("All"))
                    {
                        string attr = code.Replace("All", "");
                        var pi = type.GetProperties().Where(x => x.Name.StartsWith(attr));
                        foreach (var propertyInfo in pi)
                        {
                            int newv = (int) propertyInfo.GetValue(character.Attribute, null);
                            newv += i*sign;
                            if (newv <= 0)
                            {
                                newv = 0;
                            }
                            propertyInfo.SetValue(character.Attribute, newv, null);
                        }
                    }
                    else
                    {
                        var pi = type.GetProperty(code);
                        var t = pi.PropertyType;
                        if (t == typeof (int))
                        {
                            int newv = (int) pi.GetValue(character.Attribute, null);
                            newv += i*sign;
                            if (newv <= 0)
                            {
                                newv = 0;
                            }
                            pi.SetValue(character.Attribute, newv, null);
                        }
                        else if (t == typeof (float))
                        {
                            float newv = (float) pi.GetValue(character.Attribute, null);
                            newv += f*sign;
                            if (newv <= 0)
                            {
                                newv = 0;
                            }
                            pi.SetValue(character.Attribute, newv, null);
                        }
                    }
                    break;
                }

                    #endregion
            }
        }

        #endregion

        #region 与锻造相关的接口

        /// <summary>
        /// 类型：方法
        /// 名称：UpdateForgingAttribute
        /// 作者：taixihuase
        /// 作用：变更一条锻造属性
        /// 编写日期：2015/8/16
        /// </summary>
        /// <param name="level"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        protected virtual void UpdateForgingAttribute(int level, AttributeCode attribute, ValueType value)
        {
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UpgradeForgingAttribute
        /// 作者：taixihuase
        /// 作用：添加一条更高一级锻造等级的锻造属性，只能在调用 UpgradeCurrentLevel 前使用
        /// 编写日期：2015/8/16
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        protected virtual void UpgradeForgingAttribute(AttributeCode attribute, ValueType value)
        {
        }

        /// <summary>
        /// 类型：方法
        /// 名称：DegradeForgingAttribute
        /// 作者：taixihuase
        /// 作用：去除一条当前锻造等级的锻造属性，只能在调用 DegradeCurrentLevel 前使用
        /// 编写日期：2015/8/16
        /// </summary>
        protected virtual void DegradeForgingAttribute()
        {
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UpgradeCurrentLevel
        /// 作者：taixihuase
        /// 作用：提升锻造等级
        /// 编写日期：2015/8/20
        /// </summary>
        protected void UpgradeCurrentLevel()
        {
            if (CurrentLevel < DataConstraint.EquipmentMaxLevel)
            {
                CurrentLevel++;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：DegradeCurrentLevel
        /// 作者：taixihuase
        /// 作用：提升锻造等级
        /// 编写日期：2015/8/20
        /// </summary>
        protected void DegradeCurrentLevel()
        {
            if (CurrentLevel > 0)
            {
                CurrentLevel--;
            }
        }

        #endregion
    }
}
