//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：ZoneUpdateEventArgs.cs
//
// 文件功能描述：
//
// 角色地图区位事件数据，对角色的区位更新数据感兴趣
//
// 创建标识：taixihuase 20150917
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
using System.Collections.Generic;
using ExitGames.Client.Photon;
using SiegeOnlineServer.Protocol;
using SiegeOnlineServer.Protocol.Common;
// ReSharper disable CheckNamespace

namespace SiegeOnlineClient.ClientLogic.Event
{
    /// <summary>
    /// 类型：类
    /// 名称：ZoneUpdateEventArgs
    /// 作者：taixihuase
    /// 作用：角色区位信息更新事件数据类
    /// 编写日期：2015/9/17
    /// </summary>
    public class ZoneUpdateEventArgs : EventArgs
    {
        // 自身角色区位更新所需的数据
        public List<int> MyUpdate;

        // 其他角色区位更新所需的数据
        public MapZone OtherUpdate { get; private set; }

        /// <summary>
        /// 类型：方法
        /// 名称：ZoneUpdateEventArgs
        /// 作者：taixihuase
        /// 作用：用自身角色区位更新时所感兴趣的其他角色数据构造事件数据
        /// 编写日期：2015/9/17
        /// </summary>
        /// <param name="response"></param>
        public ZoneUpdateEventArgs(OperationResponse response)
        {
            List<int> update = (List<int>)
                Serialization.Deserialize(response.Parameters[(byte) ParameterCode.ZoneUpdate]);
            MyUpdate = update;
            OtherUpdate = null;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：ZoneUpdateEventArgs
        /// 作者：taixihuase
        /// 作用：用其他角色区位更新时所感兴趣的区位数据构造事件数据
        /// 编写日期：2015/9/17
        /// </summary>
        /// <param name="eventData"></param>
        public ZoneUpdateEventArgs(EventData eventData)
        {
            MapZone zone = (MapZone)
                Serialization.Deserialize(eventData.Parameters[(byte) ParameterCode.ZoneUpdate]);
            MyUpdate = null;
            OtherUpdate = zone;
        }
    }
}
