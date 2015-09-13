//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：RegistEventArgs.cs
//
// 文件功能描述：
//
// 注册账号事件数据，对注册操作的服务端回应数据感兴趣
//
// 创建标识：taixihuase 20150719
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
using ExitGames.Client.Photon;
// ReSharper disable CheckNamespace

namespace SiegeOnlineClient.ClientLogic.Event
{
    /// <summary>
    /// 类型：类
    /// 名称：RegistEventArgs
    /// 作者：taixihuase
    /// 作用：注册事件数据类
    /// 编写日期：2015/7/19
    /// </summary>
    public class RegistEventArgs : EventArgs
    {
        // 注册返回结果的数据
        public OperationResponse OperationResponse { get; private set; }

        /// <summary>
        /// 类型：方法
        /// 名称：RegistEventArgs
        /// 作者：taixihuase
        /// 作用：用服务端返回的注册结果构造事件数据
        /// 编写日期：2015/7/19
        /// </summary>
        /// <param name="response"></param>
        public RegistEventArgs(OperationResponse response)
        {
            OperationResponse = response;
        }
    }
}
