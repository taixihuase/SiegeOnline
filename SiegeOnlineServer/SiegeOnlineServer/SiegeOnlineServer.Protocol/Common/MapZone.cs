//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：MapZone.cs
//
// 文件功能描述：
//
// 地图区位管理，用于管理角色当前所处于的区位信息
//
// 创建标识：taixihuase 20150915
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
    /// 名称：MapZone
    /// 作者：taixihuase
    /// 作用：地图区位管理类
    /// 编写日期：2015/9/15
    /// </summary>
    [Serializable]
    public class MapZone
    {
        public MapCode CurrMap { get; set; }

        public int CurrZone { get; set; }

        public MapCode NextMap { get; set; }

        public int NextZone { get; set; }

        public int UniqueId { get; set; }
    }
}
