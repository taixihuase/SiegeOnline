//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：WorldEnterEventArgs.cs
//
// 文件功能描述：
//
// 进入游戏场景事件数据，对自身及任意玩家角色数据感兴趣
//
// 创建标识：taixihuase 20150722
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
using SiegeOnlineServer.Protocol;
using SiegeOnlineServer.Protocol.Common;
using SiegeOnlineServer.Protocol.Common.Character;
// ReSharper disable CheckNamespace

namespace SiegeOnlineClient.ClientLogic.Event
{
    /// <summary>
    /// 类型：类
    /// 名称：WorldEnterEventArgs
    /// 作者：taixihuase
    /// 作用：角色进入场景事件数据类
    /// 编写日期：2015/7/22
    /// </summary>
    public class WorldEnterEventArgs : EventArgs
    {
        // 自身角色进入场景所需的数据
        public Position MyCharacterPosition { get; private set; }

        // 任意角色进入场景所需的数据
        public Character AnyCharacter { get; private set; }

        /// <summary>
        /// 类型：方法
        /// 名称：WorldEnterEventArgs
        /// 作者：taixihuase
        /// 作用：用自身角色进入场景所感兴趣的数据构造事件数据
        /// 编写日期：2015/7/22
        /// </summary>
        /// <param name="response"></param>
        public WorldEnterEventArgs(OperationResponse response)
        {
            Position position = (Position)
                Serialization.Deserialize(response.Parameters[(byte) ParameterCode.WorldEnter]);
            MyCharacterPosition = position;
            AnyCharacter = null;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：WorldEnterEventArgs
        /// 作者：taixihuase
        /// 作用：用任意角色进入场景所感兴趣的数据构造事件数据
        /// 编写日期：2015/7/22
        /// </summary>
        /// <param name="eventData"></param>
        public WorldEnterEventArgs(EventData eventData)
        {
            Character character = (Character)
                Serialization.Deserialize(eventData.Parameters[(byte) ParameterCode.WorldEnter]);
            AnyCharacter = character;
            MyCharacterPosition = null;
        }
    }
}
