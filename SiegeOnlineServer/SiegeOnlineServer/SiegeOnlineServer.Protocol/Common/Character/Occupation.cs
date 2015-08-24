//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Occupation.cs
//
// 文件功能描述：
//
// 职业属性
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

namespace SiegeOnlineServer.Protocol.Common.Character
{
    /// <summary>
    /// 类型：类
    /// 名称：Occupation
    /// 作者：taixihuase
    /// 作用：角色职业类
    /// 编写日期：2015/7/23
    /// </summary>
    [Serializable]
    public class Occupation
    {
        public OccupationCode Type { get; protected set; } // 职业代码

        public string Name { get; protected set; } // 职业名

        public int BaseHitPoint { get; set; } // 基础生命值

        public int BaseMana { get; set; } // 基础法力值

        public int BaseLifeRecovery { get; set; } // 基础生命恢复速度

        public int BaseManaRecovery { get; set; } // 基础法力恢复速度

        private bool _apply; // 启用属性

        /// <summary>
        /// 类型：方法
        /// 名称：Occupation
        /// 作者：taixihuase
        /// 作用：构造默认职业类
        /// 编写日期：2015/8/20
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        public Occupation(OccupationCode code = OccupationCode.Common, string name = "")
        {
            Type = code;
            Name = name;
            _apply = false;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UpdateOccupation
        /// 作者：taixihuase
        /// 作用：变更职业类型
        /// 编写日期：2015/8/20
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        public void UpdateOccupation(OccupationCode code, string name)
        {
            Type = code;
            Name = name;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Apply
        /// 作者：taixihuase
        /// 作用：启用职业基础属性值
        /// 编写日期：2015/8/20
        /// </summary>
        /// <param name="attribute"></param>
        public void Apply(CharacterAttribute attribute)
        {
            if (!_apply)
            {
                attribute.HitPointBase += BaseHitPoint;
                attribute.HitPoint = (int) (attribute.HitPointBase*(100 + attribute.LifeIncreasePercent)*0.01);
                attribute.ManaBase += BaseMana;
                attribute.Mana = (int) (attribute.ManaBase*(100 + attribute.ManaIncreasePercent)*0.01);
                attribute.LifeRecovery += BaseLifeRecovery;
                attribute.ManaRecovery += BaseManaRecovery;
                _apply = true;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Cancel
        /// 作者：taixihuase
        /// 作用：禁用职业基础属性值
        /// 编写日期：2015/8/20
        /// </summary>
        /// <param name="attribute"></param>
        public void Cancel(CharacterAttribute attribute)
        {
            if (_apply)
            {
                attribute.HitPointBase -= BaseHitPoint;
                attribute.HitPoint = (int) (attribute.HitPointBase*(100 + attribute.LifeIncreasePercent)*0.01);
                attribute.ManaBase -= BaseMana;
                attribute.Mana = (int) (attribute.ManaBase*(100 + attribute.ManaIncreasePercent)*0.01);
                attribute.LifeRecovery -= BaseLifeRecovery;
                attribute.ManaRecovery -= BaseManaRecovery;
                _apply = false;
            }
        }
    }
}
