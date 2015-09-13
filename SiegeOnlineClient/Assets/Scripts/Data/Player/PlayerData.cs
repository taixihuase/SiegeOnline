//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：PlayerData.cs
//
// 文件功能描述：
//
// 玩家数据缓存
//
// 创建标识：taixihuase 20150729
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using SiegeOnlineServer.Protocol.Common.Character;
using SiegeOnlineServer.Protocol.Common.User;
// ReSharper disable CheckNamespace
// ReSharper disable EmptyConstructor

namespace SiegeOnlineClient.Data.Player
{
    /// <summary>
    /// 类型：类
    /// 名称：PlayerData
    /// 作者：taixihuase
    /// 作用：玩家数据缓存类
    /// 编写日期：2015/7/29
    /// </summary>
    public class PlayerData
    {
        // 账号信息
        public UserInfo User { get; protected set; }

        // 角色初始数据
        public Character CharacterOriginal { get; protected set; }

        // 角色数据副本
        public Character CharacterCopy { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：SetUserInfo
        /// 作者：taixihuase
        /// 作用：设置账号信息
        /// 编写日期：2015/8/27
        /// </summary>
        /// <param name="user"></param>
        public void SetUserInfo(UserInfo user)
        {
            User = user;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SetCharacterOriginal
        /// 作者：taixihuase
        /// 作用：设置角色初始数据
        /// 编写日期：2015/8/27
        /// </summary>
        /// <param name="original"></param>
        public void SetCharacterOriginal(Character original)
        {
            CharacterOriginal = new Character(original);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SetCharacterCopy
        /// 作者：taixihuase
        /// 作用：设置角色数据副本
        /// 编写日期：2015/8/27
        /// </summary>
        /// <param name="copy"></param>
        public void SetCharacterCopy(Character copy)
        {
            CharacterCopy = new Character(copy);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SetCharacter
        /// 作者：taixihuase
        /// 作用：设置角色数据
        /// 编写日期：2015/8/27
        /// </summary>
        /// <param name="original"></param>
        /// <param name="copy"></param>
        public void SetCharacter(Character original, Character copy)
        {
            SetUserInfo(new UserInfo(original.Guid, original.Account, original.UniqueId, original.Nickname, original.Status));
            SetCharacterOriginal(original);
            SetCharacterCopy(copy);
        }
    }
}

