//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：IResponseReceive.cs
//
// 文件功能描述：
//
// 处理消息回应接口，由各场景逻辑实现
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
    /// 名称：IResponseReceive
    /// 作者：taixihuase
    /// 作用：回应处理接口
    /// 编写日期：2015/7/20
    /// </summary>
    interface IResponseReceive
    {
        /// <summary>
        /// 类型：方法
        /// 名称：OnResponse
        /// 作者：taixihuase
        /// 作用：当接收到回应消息时，对消息进行处理
        /// 编写日期：2015/7/20
        /// </summary>
        /// <param name="operationResponse"></param>
        /// <param name="service"></param>
        void OnResponse(OperationResponse operationResponse, PhotonService service);
    }
}
