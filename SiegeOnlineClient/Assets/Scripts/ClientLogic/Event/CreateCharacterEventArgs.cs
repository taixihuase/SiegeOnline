//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：CreateCharacterEventArgs.cs
//
// 文件功能描述：
//
// 创建角色事件数据，对自身已登录账号数据感兴趣
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
using SiegeOnlineServer.Protocol.Common.User;
// ReSharper disable CheckNamespace

namespace SiegeOnlineClient.ClientLogic.Event
{
    /// <summary>
    /// 类型：类
    /// 名称：CreateCharacterEventArgs
    /// 作者：taixihuase
    /// 作用：创建新角色事件数据类
    /// 编写日期：2015/7/26
    /// </summary>
    public class CreateCharacterEventArgs : EventArgs
    {
        // 创建角色时所需的数据
        public UserBase User { get; private set; }

        /// <summary>
        /// 类型：方法
        /// 名称：CreateCharacterEventArgs
        /// 作者：taixihuase
        /// 作用：用创建角色时所需的数据构造事件数据
        /// 编写日期：2015/7/26
        /// </summary>
        /// <param name="user"></param>
        public CreateCharacterEventArgs(UserBase user)
        {
            User = user;
        }
    }
}
