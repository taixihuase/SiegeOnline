//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：MoveEventArgs.cs
//
// 文件功能描述：
//
// 角色位置移动事件数据，对角色位置数据感兴趣
//
// 创建标识：taixihuase 20150920
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
using SiegeOnlineServer.Protocol.Common;
// ReSharper disable CheckNamespace

namespace SiegeOnlineClient.ClientLogic.Event
{
    /// <summary>
    /// 类型：类
    /// 名称：MoveEventArgs
    /// 作者：taixihuase
    /// 作用：角色位置移动事件数据类
    /// 编写日期：2015/9/20
    /// </summary>
    public class MoveEventArgs : EventArgs
    {
        // 自身角色位置变动时所需的数据
        public OperationResponse MyMove { get; private set; }

        // 其他角色位置变动时所需的数据
        public Position OtherPosition { get; private set; }

        /// <summary>
        /// 类型：方法
        /// 名称：MoveEventArgs
        /// 作者：taixihuase
        /// 作用：用自身角色位置变化所感兴趣的数据构造事件数据
        /// 编写日期：2015/9/20
        /// </summary>
        /// <param name="response"></param>
        public MoveEventArgs(OperationResponse response)
        {
            MyMove = response;
            OtherPosition = null;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：MoveEventArgs
        /// 作者：taixihuase
        /// 作用：用其他角色位置变化所感兴趣的数据构造事件数据
        /// 编写日期：2015/9/20
        /// </summary>
        /// <param name="pos"></param>
        public MoveEventArgs(Position pos)
        {
            MyMove = null;
            OtherPosition = pos;
        }
    }
}
