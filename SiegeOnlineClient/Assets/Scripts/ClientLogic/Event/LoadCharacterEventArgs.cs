//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：LoadCharacterEventArgs.cs
//
// 文件功能描述：
//
// 加载角色事件数据，对加载的自身角色数据感兴趣
//
// 创建标识：taixihuase 20150726
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
using SiegeOnlineServer.Protocol.Common.Character;
// ReSharper disable CheckNamespace

namespace SiegeOnlineClient.ClientLogic.Event
{
    /// <summary>
    /// 类型：类
    /// 名称：LoadCharacterEventArgs
    /// 作者：taixihuase
    /// 作用：加载角色事件数据类
    /// 编写日期：2015/7/26
    /// </summary>
    public class LoadCharacterEventArgs : EventArgs
    {
        // 加载角色时所需的数据
        public Character Character { get; private set; }

        /// <summary>
        /// 类型：方法
        /// 名称：LoadCharacterEventArgs
        /// 作者：taixihuase
        /// 作用：用加载角色时所需的数据构造事件数据
        /// 编写日期：2015/7/26
        /// </summary>
        /// <param name="character"></param>
        public LoadCharacterEventArgs(Character character)
        {
            Character = character;
        }
    }
}
