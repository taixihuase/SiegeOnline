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

using System;

// ReSharper disable InconsistentNaming

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
        Attack_Both,
        Attack_Magic,
        Attack_Physical,

        // 攻击力倍率加成(百分比)
        Attack_Percent_Both,
        Attack_Percent_Magic,
        Attack_Percent_Physical,

        // 防御力加成
        Defense_Both,
        Defense_Magic,
        Defense_Physical,

        // 防御力倍率加成(百分比)
        Defense_Percent_Both,
        Defense_Percent_Magic,
        Defense_Percent_Physical,

        // 暴击率加成(百分比)
        Crit_Both,
        Crit_Magic,
        Crit_Physical,

        // 总伤害倍率加成(百分比)
        Damage_Both,
        Damage_Magic,
        Damage_Physical,

        // 暴击伤害倍率加成(百分比)
        Damage_Crit_Both,
        Damage_Crit_Magic,
        Damage_Crit_Physical,

        // 总伤害减免倍率加成(百分比)
        Reduce_Both,
        Reduce_Magic,
        Reduce_Physical,

        // 暴击伤害减免倍率加成(百分比)
        Reduce_Crit_Both,
        Reduce_Crit_Magic,
        Reduce_Crit_Physical,

        // 命中率加成(百分比)
        Hit_Both,
        Hit_Magic, 
        Hit_Physical,

        // 闪避率加成(百分比)
        Dodge_Both,
        Dodge_Magic,
        Dodge_Physical,

        // 格挡率加成(百分比)
        Block_Both,
        Block_Magic,
        Block_Physical,

        // 速率加成(百分比)
        Speed_Attack,
        Speed_Cooldown,
        Speed_Experience,
        Speed_Movement,

        // 生命加成
        Life_Increase,
        Life_Increase_Percent,
        Life_Recovery,
        Life_Steal,

        // 能量加成
        Mana_Increase,
        Mana_Increase_Percent,
        Mana_Recovery,
        Mana_Steal,

        // 潜能点加成
        Attr_Strength,
        Attr_Dexterity,
        Attr_Intellect,
        Attr_Vitality,

        // 属性抗性加成
        Resistance_All,                 // 所有抗性
        Resistance_Fire,                // 火焰抗性
        Resistance_Frost,               // 冰霜抗性
        Resistance_Lightning,           // 闪电抗性
        Resistance_Poison,              // 毒素抗性
        Resistance_Rock,                // 岩石抗性
        Resistance_Shadow,              // 暗影抗性
        Resistance_Storm,               // 风暴抗性

        // 属性增幅
        Enhance_All,                    // 所有属性
        Enhance_Fire,                   // 火焰属性
        Enhance_Frost,                  // 冰霜属性
        Enhance_Lightning,              // 闪电属性
        Enhance_Poison,                 // 毒素属性
        Enhance_Rock,                   // 岩石属性
        Enhance_Shadow,                 // 暗影属性
        Enhance_Storm,                  // 风暴属性

        // 特殊
        Feedback,                       // 魔法反馈
        Immunity,                       // 负面免疫
        Rebound,                        // 伤害反弹
    }
}
