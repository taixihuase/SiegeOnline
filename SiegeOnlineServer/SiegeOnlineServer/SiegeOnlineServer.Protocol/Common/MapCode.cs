//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：MapCode.cs
//
// 文件功能描述：
//
// 地图编号枚举
//
// 创建标识：taixihuase 20150914
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

namespace SiegeOnlineServer.Protocol.Common
{
    /// <summary>
    /// 类型：枚举
    /// 名称：MapCode
    /// 作者：taixihuase
    /// 作用：地图编号
    /// 编写日期：2015/9/14
    /// </summary>
    [Serializable]
    public enum MapCode : byte
    {
        [Description("20")]
        World,
    }
}
