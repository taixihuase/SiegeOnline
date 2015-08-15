//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：AttributeCode.cs
//
// 文件功能描述：
//
// 角色属性的参数类别
//
// 创建标识：taixihuase 20150815
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming


using System;

namespace SiegeOnlineServer.Protocol.Common
{
    /// <summary>
    /// 类型：枚举
    /// 名称：AttributeCode
    /// 作者：taixihuase
    /// 作用：角色属性参数代码枚举值
    /// 编写日期：2015/8/15
    /// </summary>
    [Serializable]
    public enum AttributeCode : byte
    {
        Null,

        // 攻击力加成
        Attack_Physical,
        Attack_Magic,
        Attack_Both,

        // 攻击力倍率加成(百分比)
        Attack_Physical_Percent,
        Attack_Magic_Percent,
        Attack_Both_Percent,

        // 防御力加成
        Defense_Physical,
        Defense_Magic,
        Defense_Both,

        // 防御力倍率加成(百分比)
        Defense_Physical_Percent,
        Defense_Magic_Percent,
        Defense_Both_Percent,

        // 暴击率加成(百分比)
        Crit_Physical,
        Crit_Magic,
        Crit_Both,

        // 总伤害倍率加成(百分比)
        Damage_Physical,
        Damage_Magic,
        Damage_Both,

        // 暴击伤害倍率加成(百分比)
        Damage_Crit_Physical,
        Damage_Crit_Magic,
        Damage_Crit_Both,

        // 总伤害减免倍率加成(百分比)
        Reduce_Physical,
        Reduce_Magic,
        Reduce_Both,

        // 暴击伤害减免倍率加成(百分比)
        Reduce_Crit_Physical,
        Reduce_Crit_Magic,
        Reduce_Crit_Both,

        // 命中率加成(百分比)
        Hit_Physical,
        Hit_Magic,
        Hit_Both,

        // 闪避率加成(百分比)
        Dodge_Physical,
        Dodge_Magic,
        Dodge_Both,

        // 格挡率加成(百分比)
        Block_Physical,
        Block_Magic,
        Block_Both,

        // 速率加成(百分比)
        Speed_Attack,
        Speed_Movement,
        Speed_Cooldown,
        Speed_Experience,

        // 生命加成
        Life_Increase,
        Life_Increase_Percent,
        Life_Recovery,
        Life_Recovery_Percent,
        Life_Steal,
        Life_Steal_Percent,

        // 能量加成
        Mana_Increase,
        Mana_Increase_Percent,
        Mana_Recovery,
        Mana_Recovery_Percent,
        Mana_Steal,
        Mana_Steal_Percent,

        // 属性点加成
        Attr_Strength,
        Attr_Dexterity,
        Attr_Vitality,
        Attr_Intellect,
    }
}
