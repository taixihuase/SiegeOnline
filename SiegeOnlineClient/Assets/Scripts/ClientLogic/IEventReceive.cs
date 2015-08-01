//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：IEventReceive.cs
//
// 文件功能描述：
//
// 处理广播事件接口，由各场景逻辑实现
//
// 创建标识：taixihuase 20150720
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using ExitGames.Client.Photon;
using SiegeOnlineClient.PhotonClient;
// ReSharper disable CheckNamespace

namespace SiegeOnlineClient.ClientLogic
{
    /// <summary>
    /// 类型：接口
    /// 名称：IEventReceive
    /// 作者：taixihuase
    /// 作用：广播处理接口
    /// 编写日期：2015/7/20
    /// </summary>
    interface IEventReceive
    {
        /// <summary>
        /// 类型：方法
        /// 名称：OnEvent
        /// 作者：taixihuase
        /// 作用：当接收到广播时，对广播进行处理
        /// 编写日期：2015/7/20
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="service"></param>
        void OnEvent(EventData eventData, PhotonService service);
    }
}
