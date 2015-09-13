//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：DataConstraint.cs
//
// 文件功能描述：
//
// 默认静态数据协议
//
// 创建标识：taixihuase 20150818
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

namespace SiegeOnlineServer.Protocol.Common
{
    /// <summary>
    /// 类型：类
    /// 名称：DataConstraint
    /// 作者：taixihuase
    /// 作用：默认常量定义
    /// 编写日期：2015/8/18
    /// </summary>
    [Serializable]
    public static class DataConstraint
    {
        // 角色最大等级
        public static int CharacterMaxLevel => 100;

        // 装备最大锻造等级
        public static int EquipmentMaxLevel => 10;

        // 装备最大耐久度
        public static int EquipmentMaxDurability => 1000;

        // 角色默认攻击速度
        public static float CharacterDefaultAttackSpeed => 100;

        // 角色默认攻击距离
        public static float CharacterDefaultAttackDistance => 1;

        // 角色默认移动速度
        public static float CharacterDefaultMovementSpeed => 100;

        // 角色默认技能冷却速度
        public static float CharacterDefaultSkillCooldownSpeed => 0;

        // 角色默认经验获得倍率
        public static float CharacterDefaultExperienceGainSpeed => 100;

        // 元素属性最大种类数
        public static int ElementMaxSize => 7;

        // 武器元素属性最大能量值
        public static int WeaponMaxElementEnergy => 1000;
    }
}
