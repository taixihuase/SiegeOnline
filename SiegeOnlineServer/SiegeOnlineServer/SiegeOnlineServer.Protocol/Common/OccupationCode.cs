//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：OccupationCode.cs
//
// 文件功能描述：
//
// 职业种类的参数类别
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
using System.ComponentModel;

// ReSharper disable InconsistentNaming

namespace SiegeOnlineServer.Protocol.Common
{
    /// <summary>
    /// 类型：枚举
    /// 名称：OccupationCode
    /// 作者：taixihuase
    /// 作用：职业类型参数代码枚举值
    /// 编写日期：2015/8/16
    /// </summary>
    [Serializable]
    [Flags]
    public enum OccupationCode : byte
    {
        Common      =   0,
        [Description("法师")]
        Mage        =   1,
        [Description("骑士")]
        Paladin     =   2,
        [Description("战士")]
        Warrior     =   4,
        [Description("巫师")]
        Wizard =   8,
    }
}
