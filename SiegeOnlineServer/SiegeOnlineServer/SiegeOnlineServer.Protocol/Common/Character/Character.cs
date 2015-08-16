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
using SiegeOnlineServer.Protocol.Common.Item;
using SiegeOnlineServer.Protocol.Common.Item.Equipment;
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
    public class Character : UserBase
    {
        public CharacterAttribute Attribute { get; set; } // 角色属性

        public Occupation Occupation { get; set; } // 职业属性

        public Dictionary<int, Weapon> Weapons; // 武器清单

        public Dictionary<int, Armor> Armors; // 防具清单

        public Dictionary<int, Jewel> Jewels; // 饰品清单 

        public Dictionary<int, Skill> Skills; // 技能清单 

        /// <summary>
        /// 类型：方法
        /// 名称：Character
        /// 作者：taixihuase
        /// 作用：通过用户信息构造一个角色对象
        /// 编写日期：2015/7/22
        /// </summary>
        /// <param name="user"></param>
        public Character(UserBase user) : base(user)
        {
            Attribute = new CharacterAttribute();
            Occupation = new Occupation();
            Weapons = new Dictionary<int, Weapon> {{-1, new Weapon()}};
            Armors = new Dictionary<int, Armor> {{-1, new Armor()}};
            Jewels = new Dictionary<int, Jewel> {{-1, new Jewel()}};
            Skills = new Dictionary<int, Skill> {{-1, new Skill()}};
        }
    }
}
