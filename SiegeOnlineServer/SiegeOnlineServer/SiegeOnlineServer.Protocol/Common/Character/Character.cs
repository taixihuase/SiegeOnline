//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Character.cs
//
// 文件功能描述：
//
// 玩家角色信息汇总，作为数据传输实例
//
// 创建标识：taixihuase 20150722
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//-----------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SiegeOnlineServer.Protocol.Common.Item.Equipment;
using SiegeOnlineServer.Protocol.Common.Skill;
using SiegeOnlineServer.Protocol.Common.User;

namespace SiegeOnlineServer.Protocol.Common.Character
{
    /// <summary>
    /// 类型：类
    /// 名称：Character
    /// 作者：taixihuase
    /// 作用：玩家属性汇总类
    /// 编写日期：2015/7/22
    /// </summary>
    [Serializable]
    public class Character : UserInfo
    {
        public DateTime WorldEnterTime { get; set; }

        public Position Position { get; set; } // 位置信息

        public Experience Experience { get; set; } // 经验信息

        public Occupation Occupation { get; set; } // 职业属性

        public CharacterAttribute Attribute { get; set; } // 角色属性

        public WeaponCollection Weapons { get; set; } // 武器清单

        public ArmorCollection Armors { get; set; } // 防具清单

        public JewelCollection Jewels { get; set; } // 饰品清单 

        public SkillCollection Skills { get; set; } // 技能清单 

        /// <summary>
        /// 类型：方法
        /// 名称：Character
        /// 作者：taixihuase
        /// 作用：通过用户信息构造一个角色对象
        /// 编写日期：2015/7/22
        /// </summary>
        /// <param name="user"></param>
        public Character(UserInfo user) : base(user)
        {
            Position = new Position();
            Experience = new Experience();
            Occupation = new Occupation();
            Attribute = new CharacterAttribute();
            Weapons = new WeaponCollection();
            Armors = new ArmorCollection();
            Jewels = new JewelCollection();
            Skills = new SkillCollection();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Character
        /// 作者：taixihuase
        /// 作用：通过已有的角色信息构造一个新的角色实例
        /// 编写日期：2015/8/20
        /// </summary>
        /// <param name="character"></param>
        public Character(Character character)
            : base(character.Guid, character.Account, character.UniqueId, character.Nickname, character.Status)
        {
            WorldEnterTime = character.WorldEnterTime;
            Position = character.Position;
            Experience = character.Experience;
            Occupation = character.Occupation;
            Weapons = character.Weapons;
            Armors = character.Armors;
            Jewels = character.Jewels;
            Skills = character.Skills;
            Attribute = new CharacterAttribute();
            Type type = character.Attribute.GetType();
            var pi = type.GetProperties();
            foreach (var info in pi)
            {
                info.SetValue(Attribute, info.GetValue(character.Attribute, null), null);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：ApplyEquipments
        /// 作者：taixihuase
        /// 作用：启用装备属性
        /// 编写日期：2015/8/20
        /// </summary>
        public void ApplyEquipments()
        {
            Armors.Apply(this);
            Jewels.Apply(this);
            Weapons.Apply(this);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CancelEquipments
        /// 作者：taixihuase
        /// 作用：禁用装备属性
        /// 编写日期：2015/8/20
        /// </summary>
        public void CancelEquipments()
        {
            Armors.Cancel(this);
            Jewels.Cancel(this);
            Weapons.Cancel(this);
        }
    }
}
