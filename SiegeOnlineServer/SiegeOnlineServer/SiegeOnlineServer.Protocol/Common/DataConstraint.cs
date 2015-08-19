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
        public static int EquipmentMaxLevel => 10;

        public static int EquipmentMaxDurability => 1000;

        public static float CharacterDefaultAttackSpeed => 100;

        public static float CharacterDefaultAttackDistance => 1;

        public static float CharacterDefaultMovementSpeed => 100;

        public static float CharacterDefaultSkillCooldownSpeed => 100;

        public static float CharacterDefaultExperienceGainSpeed => 100;
    }
}
